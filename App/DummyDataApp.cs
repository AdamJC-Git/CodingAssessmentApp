using CodingAssessmentApp.Logging;
using CodingAssessmentApp.Models.DTO;
using CodingAssessmentApp.Services.IServices;
using CodingAssessmentApp.UserInteraction.IUserInteraction;
using Newtonsoft.Json;

namespace CodingAssessmentApp.App
{
    public class DummyDataApp
    {
        private readonly ILogger _logger;
        private readonly IUserInteractor _userInteractor;        
        private readonly IAuthService _authService;
        private readonly IProductService _productService;
        public DummyDataApp(ILogger logger, IUserInteractor userInteractor, IAuthService authService, IProductService productService)
        {
            _logger = logger;
            _userInteractor = userInteractor;
            _authService = authService;
            _productService = productService;
        }

        public async Task Run()
        {
            //_logger.LogInformation("Application started.");

            LoginResponseDTO loginResponseDto = RunUserAuthentication().Result;
            ProductsResponseDTO productsList = RunReturnTopThreeExpensivePhones(loginResponseDto.accessToken);
            RunReturnTopThreeExpensivePhonesPriceUpdate(loginResponseDto.accessToken, productsList);
        }

        private async Task<LoginResponseDTO> RunUserAuthentication()
        {
            LoginResponseDTO loginResponseDto = new();
            (string username, string password) userLoginCredentials = ValidateUserLoginCredentials();
            var loginRequestDto = ConfigureLoginRequestDto(userLoginCredentials.username, userLoginCredentials.password);

            var response = await _authService.LoginAsync<string>(loginRequestDto);
            if (response != null)
            {
                loginResponseDto = JsonConvert.DeserializeObject<LoginResponseDTO>(response);
                if(loginResponseDto.message == "Invalid credentials")
                {
                    _userInteractor.PrintMessage("Error: Authentication failed. Please restart application and re-enter login details.");
                    //_logger.Log("Error: Authentication failed. Please restart application and re-enter login details.")
                    Console.ReadKey();
                    System.Environment.Exit(0);
                }

                _userInteractor.PrintMessage($"Successful authentication for User: {loginResponseDto.username}.");
                _userInteractor.PrintMessage(Environment.NewLine);
                //_logger.Log($"Successful authentication for User: {loginResponseDto.username}.");
            }
            
            return loginResponseDto;
        }

        private ProductsResponseDTO RunReturnTopThreeExpensivePhones(string token)
        {
            try
            {
                ProductsResponseDTO productsList = new ();
                var response = _productService.GetThreeMostExpensivePhonesAsync<string>(token);
                if (response != null)
                {
                    productsList = JsonConvert.DeserializeObject<ProductsResponseDTO>(Convert.ToString(response.Result));
                    _userInteractor.PrintMessage("Top 3 Most Expensive Smart phones:");
                    foreach (var product in productsList.products)
                    {
                        _userInteractor.PrintMessage($"Product: {product.title} - Brand: {product.brand} - Price: {product.price}");
                    }
                }
                return productsList;
            }
            catch (Exception ex)
            {
                _userInteractor.PrintMessage($"{ex.Message}: Inner Exception: {ex.InnerException} - {ex.StackTrace}");
                //_logger.LogError($"{ex.Message}: Inner Exception: {ex.InnerException} - {ex.StackTrace}");
                return null;
            }                
        }

        private void RunReturnTopThreeExpensivePhonesPriceUpdate(string token, ProductsResponseDTO productsList)
        {
            (bool isValid, int result) isUserInputValid = ValidatePercentageAmountEntered();

            if(isUserInputValid.isValid)
            { 
                foreach(var product in productsList.products)
                {
                    ProductPriceDTO priceDto = new();
                    priceDto.price = Math.Round(((product.price / 100) * isUserInputValid.result) + product.price, 2);
                    var response = _productService.UpdateThreeMostExpensivePhonesAsync<string>(token, product.id, priceDto);

                    if (response != null)
                    {
                        ProductDTO updatedProduct = JsonConvert.DeserializeObject<ProductDTO>(Convert.ToString(response.Result));

                        _userInteractor.PrintMessage($"Product: {updatedProduct.title} - Brand: {updatedProduct.brand} - Price: {updatedProduct.price}");
                    }
                }                
            }
            _userInteractor.PrintMessage("Application completed...");
            Console.ReadKey();
        }

        private ValueTuple<string, string> ValidateUserLoginCredentials()
        {
            _userInteractor.PrintMessage("Welcome to User Data App:");
            _userInteractor.PrintMessage("Please enter your username:");

            var username = Console.ReadLine();

            _userInteractor.PrintMessage("Please enter your password:");
            var password = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                _userInteractor.PrintMessage("Invalid username or password. Please restart application and re-enter login details.");
                Console.ReadKey();
                System.Environment.Exit(0);
            }

            return (username, password);
        }

        private LoginRequestDTO ConfigureLoginRequestDto(string username, string password)
        {
            return new LoginRequestDTO()
            {
                username = username,
                password = password
            };
        }

        private ValueTuple<bool, int> ValidatePercentageAmountEntered()
        {
            _userInteractor.PrintMessage(Environment.NewLine);
            _userInteractor.PrintMessage("Please enter a percentage for products price increase (enter a number between 0 and 100):");
            var percentageIncrease = Console.ReadLine();
            _userInteractor.PrintMessage(Environment.NewLine);

            //Update prices by percentage
            var isPercentageNumeric = int.TryParse(percentageIncrease, out int result);
            if (isPercentageNumeric && percentageIncrease != null)
            {
                if (result < 0 || result > 100)
                {
                    _userInteractor.PrintMessage("Input error: Percentage is either less than 0 or more than 100.");
                    return (false, 0);
                }
                _userInteractor.PrintMessage($"Updated Prices for Smart phones: Increase of {percentageIncrease}%");
                return (true, result);
            }
            _userInteractor.PrintMessage("Input error: Incorrect value entered for percentage. Please restart the application.");
            return (false, 0);
        }       
    }
}
