using PactNet.Infrastructure.Outputters;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit.Abstractions;

namespace ProviderTemplate.PactTest.XUnitHelpers
{
    public class XUnitOutput : IOutput
    {
        private readonly ITestOutputHelper _output;

        public XUnitOutput(ITestOutputHelper output)
        {
            _output = output;
        }

        public void WriteLine(string line)
        {
            _output.WriteLine(line);
        }
    }
}
