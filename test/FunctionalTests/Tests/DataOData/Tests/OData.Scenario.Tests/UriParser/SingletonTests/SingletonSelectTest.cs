﻿//---------------------------------------------------------------------
// <copyright file="SingletonSelectTest.cs" company="Microsoft">
//      Copyright (C) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.
// </copyright>
//---------------------------------------------------------------------

namespace Microsoft.Test.Taupo.OData.Scenario.Tests.UriParser
{
    using System.Runtime.CompilerServices;
    using Microsoft.OData.Core;
    using Microsoft.OData.Core.UriParser;
    using Microsoft.OData.Edm;
    using Microsoft.Test.Taupo.OData.Common;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class SingletonSelectTest : UriParserTestsBase
    {
        [TestMethod]
		[MethodImplAttribute(MethodImplOptions.NoOptimization)]
        public void SelectBasic()
        {
            var selectString = "FirstName";
            ODataUriParser parser = this.CreateSelectUriParser(bossBase, selectString);
            var result = parser.ParseSelectAndExpand();
            ApprovalVerify(QueryNodeToStringVisitor.GetTestCaseAndResultString(result, selectString, null));
        }

        [TestMethod]
		[MethodImplAttribute(MethodImplOptions.NoOptimization)]
        public void SelectComplex()
        {
            var selectString = "HomeAddress";
            ODataUriParser parser = this.CreateSelectUriParser(bossBase, selectString);
            var result = parser.ParseSelectAndExpand();
            ApprovalVerify(QueryNodeToStringVisitor.GetTestCaseAndResultString(result, selectString, null));
        }

        [TestMethod]
		[MethodImplAttribute(MethodImplOptions.NoOptimization)]
        public void SelectNavigation()
        {
            var selectString = "Parent";
            ODataUriParser parser = this.CreateSelectUriParser(bossBase, selectString);
            var result = parser.ParseSelectAndExpand();
            ApprovalVerify(QueryNodeToStringVisitor.GetTestCaseAndResultString(result, selectString, null));
        }

        [TestMethod]
		[MethodImplAttribute(MethodImplOptions.NoOptimization)]
        public void SelectContainedNavigation()
        {
            var selectString = "Child";
            ODataUriParser parser = this.CreateSelectUriParser(bossBase, selectString);
            var result = parser.ParseSelectAndExpand();
            ApprovalVerify(QueryNodeToStringVisitor.GetTestCaseAndResultString(result, selectString, null));
        }

        [TestMethod]
		[MethodImplAttribute(MethodImplOptions.NoOptimization)]
        public void SelectFunction()
        {
            var selectString = "Microsoft.Test.Taupo.OData.WCFService.GetChild";
            ODataUriParser parser = this.CreateSelectUriParser(bossBase, selectString);
            var result = parser.ParseSelectAndExpand();
            ApprovalVerify(QueryNodeToStringVisitor.GetTestCaseAndResultString(result, selectString, null));
        }

        [TestMethod]
		[MethodImplAttribute(MethodImplOptions.NoOptimization)]
        public void SelectAction()
        {
            var selectString = "Microsoft.Test.Taupo.OData.WCFService.GetBrothers";
            ODataUriParser parser = this.CreateSelectUriParser(bossBase, selectString);
            var result = parser.ParseSelectAndExpand();
            ApprovalVerify(QueryNodeToStringVisitor.GetTestCaseAndResultString(result, selectString, null));
        }

        [TestMethod]
		[MethodImplAttribute(MethodImplOptions.NoOptimization)]
        public void SelectAll()
        {
            var selectString = "*";
            ODataUriParser parser = this.CreateSelectUriParser(bossBase, selectString);
            var result = parser.ParseSelectAndExpand();
            ApprovalVerify(QueryNodeToStringVisitor.GetTestCaseAndResultString(result, selectString, null));
        }

        [TestMethod]
		[MethodImplAttribute(MethodImplOptions.NoOptimization)]
        public void SelectDoesNotExist()
        {
            var selectString = "SpaghettiSquash";
            ODataUriParser parser = this.CreateSelectUriParser(bossBase, selectString);
            try
            {
                parser.ParseSelectAndExpand();
            }
            catch (ODataException e)
            {
                var expected = ODataExpectedExceptions.ODataException("MetadataBinder_PropertyNotDeclared", this.employee.FullName(), "SpaghettiSquash");
                expected.ExpectedMessage.Verifier.VerifyMatch("MetadataBinder_PropertyNotDeclared", e.Message, this.employee.FullName(), "SpaghettiSquash");
            }
        }

        [TestMethod]
		[MethodImplAttribute(MethodImplOptions.NoOptimization)]
        public void SelectQueryOption()
        {
            var selectString = "$batch";
            ODataUriParser parser = this.CreateSelectUriParser(bossBase, selectString);
            try
            {
                parser.ParseSelectAndExpand();
            }
            catch (ODataException e)
            {
                var expected = ODataExpectedExceptions.ODataException("UriSelectParser_SystemTokenInSelectExpand", "$batch", "$batch");
                expected.ExpectedMessage.Verifier.VerifyMatch("UriSelectParser_SystemTokenInSelectExpand", e.Message, "$batch", "$batch");
            }
        }
    }
}
