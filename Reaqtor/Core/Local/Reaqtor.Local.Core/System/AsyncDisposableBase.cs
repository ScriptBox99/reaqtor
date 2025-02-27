﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT License.
// See the LICENSE file in the project root for more information.

//
// Revision history:
//
// BD - June 2013 - Created this file.
//

using System.Threading;
using System.Threading.Tasks;

namespace System
{
    /// <summary>
    /// Base class for asynchronous resource disposables.
    /// </summary>
    public abstract class AsyncDisposableBase : IAsyncDisposable
    {
#if !NET5_0 && !NETSTANDARD2_1
        /// <summary>
        /// Disposes the resource asynchronously.
        /// </summary>
        /// <param name="token">Token to observe for cancellation of the disposal request.</param>
        /// <returns>Task representing the eventual completion of the disposal request.</returns>
        public Task DisposeAsync(CancellationToken token)
        {
            //
            // TODO: Enforce idempotent behavior? What about transient errors, cancellations, etc?
            //       Should be allow retry but no concurrent calls? Do we rely on DisposeAsyncCore
            //       being idempotent itself?
            //
            return DisposeAsyncCore(token);
        }
#else
        /// <summary>
        /// Disposes the resource asynchronously.
        /// </summary>
        /// <returns>Task representing the eventual completion of the disposal request.</returns>
        public ValueTask DisposeAsync()
        {
            //
            // TODO: Enforce idempotent behavior? What about transient errors, cancellations, etc?
            //       Should be allow retry but no concurrent calls? Do we rely on DisposeAsyncCore
            //       being idempotent itself?
            //
            return new ValueTask(DisposeAsyncCore(default));
        }
#endif

        /// <summary>
        /// Disposes the resource asynchronously.
        /// </summary>
        /// <param name="token">Token to observe for cancellation of the disposal request.</param>
        /// <returns>Task representing the eventual completion of the disposal request.</returns>
        protected abstract Task DisposeAsyncCore(CancellationToken token);
    }
}
