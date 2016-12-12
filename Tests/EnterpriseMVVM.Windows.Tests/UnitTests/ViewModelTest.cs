using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using EnterpriseMVVM.Windows;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EnterpriseMVVM.Windows.Tests.UnitTests
{
    [TestClass]
    public class ViewModelTest
    {
        [TestMethod]
        public void IsAbstractBaseClass()
        {
            Type t = typeof(ViewModel);

            Assert.IsTrue(t.IsAbstract);
        }

        [TestMethod]       
        public void IsDataErrorInfo()
        {
            Assert.IsTrue(typeof(IDataErrorInfo).IsAssignableFrom(typeof(ViewModel)));
        }

        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void IDataErrorInfo_ErrorProperty_IsNotSuported()
        {
            var viewModel = new StubViewModel();
            var value = viewModel.Error;
        }

        [TestMethod]
        public void IsObservableObject()
        {
            Assert.IsTrue(typeof(ViewModel).BaseType == typeof(ObservableObject));
        }


        [TestMethod]
        public void IndexPropertyValidatesPropertyNameWithInvalidValue()
        {
            var viewModel = new StubViewModel()
            {
                RequiredProporty = "Some Value"
            };

            Assert.IsNull(viewModel["RequiredProperty"]);
        }

        [TestMethod]
        public void IndexPropertyValidatesPropertyNameWithValidValue()
        {
            var viewModel = new StubViewModel
                            {

                                RequiredProporty = "Some Value"
                            };

            Assert.IsNull(viewModel["RequiredProperty"]);
        }


        [TestMethod]
        public void IndexReturnsErrorMessageForRequestInvalidProperty()
        {
            var viewModel = new StubViewModel
            { 
                RequiredProporty = null,
                SomeProperty = null
            };

            var msg = viewModel["SomeOtherProperty"];

            Assert.IsNull(msg);
        } 

        class StubViewModel : ViewModel
        {
            [Required]
            public string RequiredProporty
            {
                get; 
                set;
            }

            [Required]
            public string SomeProperty
            {
                get;
                set;
            }

        }
    }
}
