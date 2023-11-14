# Tafs.Activities.Mediator

[Mediator](https://github.com/martinothamar/Mediator)-like library in UiPath!

Based on the source-generated library Mediator, this UiPath library strives to add messages and requests to UiPath. This won't be a fully-featured library, especially to start, but the intent is that an activity can be provided which will accept an message and fire a handler for that message. For simplicity, the message itself will be passed to the event handler, which will always be generated in a Transient context.

## The GenericRequest&lt;T&gt; type

A new GenericRequest<> type has been created for use with UiPath.

```cs
public sealed record class GenericRequest(RequestArguments RequestArguments);

public sealed record class GenericRequest<out TResponse>(RequestArguments RequestArguments);
```
