﻿using System;
using System.IO;
using System.Linq;
using WebCompiler;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WebCompilerTest
{
    [TestClass]
    public class ScssTest
    {
        private ConfigFileProcessor _processor;

        [TestInitialize]
        public void Setup()
        {
            _processor = new ConfigFileProcessor();
        }

        [TestCleanup]
        public void Cleanup()
        {
            File.Delete("../../artifacts/scss/test.css");
            File.Delete("../../artifacts/scss/test.min.css");
        }

        [TestMethod, TestCategory("SCSS")]
        public void CompileScss()
        {
            var result = _processor.Process("../../artifacts/scssconfig.json");
            Assert.IsTrue(File.Exists("../../artifacts/scss/test.css"));
        }

        [TestMethod, TestCategory("SCSS")]
        public void CompileScssError()
        {
            var result = _processor.Process("../../artifacts/scssconfigError.json");
            Assert.IsTrue(result.Count() == 1);
            Assert.IsTrue(result.ElementAt(0).HasErrors);
        }

        [TestMethod, TestCategory("SCSS")]
        public void AssociateExtensionSourceFileChangedTest()
        {
            var result = _processor.SourceFileChanged("../../artifacts/scssconfig.json", "scss/test.scss");
            Assert.AreEqual(1, result.Count<CompilerResult>());
        }

        [TestMethod, TestCategory("SCSS")]
        public void OtherExtensionTypeSourceFileChangedTest()
        {
            var result = _processor.SourceFileChanged("../../artifacts/scssconfig.json", "scss/filewithinvalidextension.less");
            Assert.AreEqual(0, result.Count<CompilerResult>());
        }
    }
}
