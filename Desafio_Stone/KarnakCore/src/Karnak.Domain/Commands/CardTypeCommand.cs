using Karnak.Domain.Core.Commands;
using System;

namespace Karnak.Domain.Commands
{
    public abstract class CardTypeCommand : Command
    {
        public Guid Id { get; protected set; }

        public string Name { get; protected set; }
    }
}
