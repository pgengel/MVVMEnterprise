using System;
using System.ComponentModel;
using System.Runtime.Remoting.Channels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
//using EnterpriseMVVM.Windows.Fakes;
using EnterpriseMVVM.Windows;

namespace EnterpriseMVVM.Windows.Tests.UnitTests
{
    [TestClass]
    public class ObservableObjectTest
    {
        [TestMethod]
        public void PropertyChangeEventHandlerIsRaised()
        {
            var obj = new StubObservableObject();

            bool raised = false;

            obj.PropertyChanged += (sender, e) =>
            {
                Assert.IsTrue(e.PropertyName == "ChangedProperty");
                raised = true;
            };

            obj.ChangedProperty = "Some Value";

            if (!raised) Assert.Fail("Property was never invoked.");

        }

        class StubObservableObject : ObservableObject
        {
            private string changeProperty;

            public string ChangedProperty
            {
                get { return changeProperty; }
                set
                {
                    changeProperty = value;
                    NotifyPropertyChanged();
                }
            }
        }
    }
}
