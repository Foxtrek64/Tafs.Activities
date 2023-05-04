# Tafs.Activities

This library is a collection of Windows Workflow Foundation and CoreWF activities as well as helper and extension methods for use in development.

Use of this library is free under the AGPL-3.0 license.

## Libraries

* `Tafs.Activities.ActivityBase` - Provides [`AsyncTaskCodeActivity`](blob/main/Tafs.Activities.ActivityBase/AsyncTaskCodeActivity.cs), a wrapper around the built-in `AsyncCodeActivity`, which allows Activity exection using a `Task`. Also comes with a generic implementation using `Task<TEntity>`.
    * Targets: Net461, Net462.
    * For CoreWF (Net6.0 and newer), `AsyncTaskCodeActivity` and `AsyncTaskCodeActivity<TResult>` are [built-in](https://github.com/UiPath/CoreWF/pull/149).
* `Tafs.Activities.Finance` - Provides a set of models used for financial calculations, including the Social Security Number type.
* `Tafs.Activities.Results.Extensions` - Provides a set of commonly used error types for use with `Remora.Results`.
* `Tafs.Activities.SecureStringConverter` - Provides two extension methods, one for converting a `string` to a `SecureString` and one for converting a `SecureString` to a `string`.
* `Tafs.Activities.Serialization` - Provides support classes for deserializing JSON data using System.Text.Json. Primarily intended for use in custom activities, but the included models for `Optional<TEntity>` and `SocialSecurityNumber` may prove useful to some.
* `Tafs.Activities.TafsAPI` - Provides a set of activities for interfacing with the TAFS API.