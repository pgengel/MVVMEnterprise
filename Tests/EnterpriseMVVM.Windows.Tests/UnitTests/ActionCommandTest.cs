using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EnterpriseMVVM.Windows.Tests.UnitTests
{
    [TestClass]
    public class ActionCommandTest
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ConstructorThrowExceptionIfActionParameterIsNull()
        {
            var command = new ActionCommand(null);
        }

        [TestMethod]
        public void ExecuteInvokesAction()
        {
            var invoke = false;

            Action<Object> action = obj => invoke = true;

            var command = new ActionCommand(action);

            command.Execute();

            Assert.IsTrue(invoke);
        }

        [TestMethod]
        public void ExecuteOverLoadInvokesActionWithParameter()
        {
            var invoke = false;

            Action<Object> action = obj =>
            {
                Assert.IsNotNull(obj);
                invoke = true;
            };

            var command = new ActionCommand(action);

            command.Execute(new object());

            Assert.IsTrue(invoke);
        }

        [TestMethod]
        public void CanExecuteIsTrueByDefault()
        {
            var command = new ActionCommand(obj => { });
            Assert.IsTrue(command.CanExecute(null));
        }

        [TestMethod]
        public void CanExecuteOverloadExecuteTruePredicate()
        {
            var command = new ActionCommand(obj => { }, obj => (int)obj == 1);
            Assert.IsTrue(command.CanExecute(1));
        }

        [TestMethod]
        public void CanExecuteOverloadExecuteFalsePredicate()
        {
            var command = new ActionCommand(obj => { }, obj => (int)obj == 1);
            Assert.IsFalse(command.CanExecute(0));
        }
    }
}
