using Plugin.ValidationRules.Extensions;
using Plugin.ValidationRules;
using ValidationRulesTest.Validations;
using ValidationRulesTest.Models;
using Plugin.ValidationRules.Formatters;
using Xamarin.Forms;
using System.Windows.Input;

namespace ValidationRulesTest.ViewModels
{
    public class Example4ViewModel : ExtendedPropertyChanged
    {

        public Example4ViewModel()
        {
            _user = new UserValidator();

            _user.Name.ValueFormatter = new StringCaseFormatter(StringCases.Capitalize);
            _user.LastName.ValueFormatter = new StringCaseFormatter(StringCases.Upper);
        }

        private UserValidator _user;
        public UserValidator User
        {
            get => _user;
            set => SetProperty(ref _user, value);
        }

        public ICommand FillFormCommand => new Command(ExecuteFillFormCommand);

        void ExecuteFillFormCommand()
        {
            User.Name.Value = "Luis";
            User.LastName.Value = "Matos";
            User.Email.Value = "luismatos@luismts.com";
        }

        public bool Validate()
        {
            // Your logic goes here
            MappingTest();
            // Your logic goes here

            return User.Validate();
        }

        private void MappingTest()
        {
            var stopper = new System.Diagnostics.Stopwatch();
            var testRuns = 1000; // 1 second

            stopper.Start();

            User modelUser = User.Map();

            stopper.Stop();

            var time1 = stopper.Elapsed.TotalMilliseconds / (double)testRuns;
            System.Console.WriteLine("ManualMapper: " + time1);             // Elapsed time: 0.002

            stopper.Restart();

            // Extension Mapper with simple Model
            var extMapperUser = User.MapValidator<User, UserValidator>();

            stopper.Stop();

            var time2 = stopper.Elapsed.TotalMilliseconds / (double)testRuns;
            System.Console.WriteLine("ExtensionMapper: " + time2);          // Elapsed time: 0.013
        }

    }
}
