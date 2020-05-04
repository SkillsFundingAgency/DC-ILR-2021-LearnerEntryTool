using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace IlrLearnerEntry.UIAutomation.Tests.TestHelper
{
    [CollectionDefinition("Application Collection")]
   public class ApplicationCollection: ICollectionFixture<ApplicationFixture>
    {
    }
}
