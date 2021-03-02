using System;

namespace OpenFoodFacts.Common.Exceptions
{
    public class IntegrationException:Exception
    {
        public IntegrationException(string message) : base(message)
        {
        }
    }
}
