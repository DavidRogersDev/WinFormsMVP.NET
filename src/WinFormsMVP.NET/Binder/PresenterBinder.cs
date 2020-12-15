using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Microsoft.Extensions.Logging;
using WinFormsMVP.NET.Messaging;

namespace WinFormsMVP.NET.Binder
{
    /// <summary/>
    public sealed class PresenterBinder
    {
        private static IPresenterDiscoveryStrategy _discoveryStrategy;
        private static IPresenterFactory _factory;
        private static IKeyValueState _keyValueState;
        private static IMessageBus _messageBus;

        ///<summary>
        /// Gets or sets the factory that the binder will use to create
        /// new presenter instances. This is pre-initialized to a
        /// default implementation but can be overriden if desired.
        /// This property can only be set once.
        ///</summary>
        ///<exception cref="ArgumentNullException">Thrown if a null value is passed to the setter.</exception>
        ///<exception cref="InvalidOperationException">Thrown if the property is being set for a second time.</exception>
        public static IPresenterFactory Factory
        {
            get => _factory ??= new DefaultPresenterFactory();
            set
            {
                if (_factory != null)
                {
                    throw new InvalidOperationException(
                        _factory is DefaultPresenterFactory
                            ? "The factory has already been set, and can be not changed at a later time. In this case, it has been set to the default implementation. This happens if the factory is used before being explicitly set. If you wanted to supply your own factory, you need to do this in your Application_Start event."
                            : "You can only set your factory once, and should really do this in Application_Start.");
                }
                _factory = value ?? throw new ArgumentNullException(nameof(value));
            }
        }


        public static ILogger TraceLogger { get; set; }


        ///<summary>
        /// Gets or sets the strategy that the binder will use to discover which presenters should be bound to which views.
        /// This is pre-initialized to a default implementation but can be overriden if desired. To combine multiple
        /// strategies in a fallthrough approach, use <see cref="CompositePresenterDiscoveryStrategy"/>.
        ///</summary>
        ///<exception cref="ArgumentNullException">Thrown if a null value is passed to the setter.</exception>
        public static IPresenterDiscoveryStrategy DiscoveryStrategy
        {
            get
            {
                //return discoveryStrategy ?? (discoveryStrategy = new AttributeBasedPresenterDiscoveryStrategy());
                return _discoveryStrategy ??= new CompositePresenterDiscoveryStrategy(
                    new AttributeBasedPresenterDiscoveryStrategy(),
                    new ConventionBasedPresenterDiscoveryStrategy()
                );
            }
            set => _discoveryStrategy = value ?? throw new ArgumentNullException(nameof(value));
        }


        public static IKeyValueState ApplicationState => _keyValueState ??= new KeyValueState();

        public static IMessageBus MessageBus => _messageBus ??= new MessageBus();

        /// <summary>
        /// Occurs when the binder creates a new presenter instance. Useful for
        /// populating extra information into presenters.
        /// </summary>
        public event EventHandler<PresenterCreatedEventArgs> PresenterCreated;


        /// <summary>
        /// Initializes a new instance of the <see cref="PresenterBinder"/> class.
        /// </summary>
        /// <param name="host">The host.</param>
        /// <param name="httpContext">The owning HTTP context.</param>
        public PresenterBinder()
        {
            
        }

        /// <summary>
        /// Attempts to bind any already registered views.
        /// </summary>
        public void PerformBinding(IView viewInstance)
        {
            try
            {
                PerformBinding(
                    viewInstance,
                    DiscoveryStrategy,
                    p => OnPresenterCreated(new PresenterCreatedEventArgs(p)),
                    Factory);

            }   
            catch (Exception e)
            {
                    
            }
        }

        private void OnPresenterCreated(PresenterCreatedEventArgs args)
        {
            PresenterCreated?.Invoke(this, args);
        }

        private static IPresenter PerformBinding(
            IView candidate,
            IPresenterDiscoveryStrategy presenterDiscoveryStrategy,
            Action<IPresenter> presenterCreatedCallback,
            IPresenterFactory presenterFactory)
        {
            var bindings = GetBindings(
                candidate,
                presenterDiscoveryStrategy);

            var newPresenter = BuildPresenter(
                presenterCreatedCallback,
                presenterFactory,
                new[] {bindings});

            return newPresenter;
        }

        private static PresenterBinding GetBindings(IView candidate,
            IPresenterDiscoveryStrategy presenterDiscoveryStrategy)
        {
            TraceLogger?.TraceMessageFindingBindings(presenterDiscoveryStrategy.GetType().Name);

            var result = presenterDiscoveryStrategy.GetBinding(candidate);

            TraceLogger?.TraceBindingInfo(BuildTraceMessagesForBindings(presenterDiscoveryStrategy, result));

            ThrowExceptionsForViewsWithNoPresenterBound(result);

            return result.Bindings.Single();
        }

        private static void ThrowExceptionsForViewsWithNoPresenterBound(PresenterDiscoveryResult result)
        {
            if (result.Bindings.Empty() && result.ViewInstances.Any(v => v.ThrowExceptionIfNoPresenterBound))
                throw new InvalidOperationException(string.Format(
                    CultureInfo.InvariantCulture,
                    @"Failed to find presenter for view instance of {0}.{1} If you do not want this exception to be thrown, set ThrowExceptionIfNoPresenterBound to false on your view.",
                    result.ViewInstances.Single(v => v.ThrowExceptionIfNoPresenterBound).GetType().FullName,
                    result.Message
                    ));
        }

        private static IPresenter BuildPresenter(
            Action<IPresenter> presenterCreatedCallback,
            IPresenterFactory presenterFactory,
            IEnumerable<PresenterBinding> bindings)
        {
            return bindings
                .Select(binding =>
                    BuildPresenters(
                        presenterCreatedCallback,
                        presenterFactory,
                        binding)).ToList().First();
        }

        private static IPresenter BuildPresenters(
            Action<IPresenter> presenterCreatedCallback,
            IPresenterFactory presenterFactory,
            PresenterBinding binding)
        {
            IView viewToCreateFor = binding.ViewInstance;

            return BuildPresenter(
                presenterCreatedCallback,
                presenterFactory,
                binding,
                viewToCreateFor);
        }

        private static IPresenter BuildPresenter(
            Action<IPresenter> presenterCreatedCallback,
            IPresenterFactory presenterFactory,
            PresenterBinding binding,
            IView viewInstance)
        {
            var presenterTypeName = binding.PresenterType.FullName;
            var viewTypeName = binding.ViewType.FullName;
            var viewInstanceName = viewInstance.GetType().FullName;

            TraceLogger?.TraceActualBindingDetails(
                presenterTypeName,
                viewTypeName,
                viewInstanceName
            );

            var presenter = presenterFactory.Create(binding.PresenterType, binding.ViewType, viewInstance);

            presenterCreatedCallback?.Invoke(presenter);

            return presenter;
        }

        private static string BuildTraceMessagesForBindings(
            IPresenterDiscoveryStrategy presenterDiscoveryStrategy,
            PresenterDiscoveryResult result)
        {
            var strategyName = presenterDiscoveryStrategy.GetType().FullName;

            return string.Format(
                CultureInfo.InvariantCulture,
                @"Found a presenter binding for {0} using {1}.{2}{3}",
                string.Join(", ", result.ViewInstances.Select(v => v.GetType().FullName).ToArray()),
                strategyName,
                $"{Environment.NewLine}{Environment.NewLine}{result.Message}{Environment.NewLine}",
                $"{Environment.NewLine}{string.Join(Environment.NewLine, result.Bindings.Select(b => string.Format(CultureInfo.InvariantCulture, @"Presenter type: {0} {1}View type: {2}", b.PresenterType.FullName, Environment.NewLine, b.ViewType.FullName)).ToArray())}"
            );
        }
    }
}