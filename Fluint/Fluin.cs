using BibliTech.I18n;
using BibliTech.I18n.Builder;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fluint
{

    public static class Fluin
    {
        public static IInProvider? Provider { get; private set; }

        public static void Initialize(Action<InBuilder> builderAction)
        {
            var builder = new InBuilder();
            builderAction(builder);

            Provider = builder.Build();
        }

    }

}
