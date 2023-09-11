using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReactFridgeTests.TestData
{
    public class RecipeData
    {
        public static string RecipeJSON = @"{
                    ""RecipeData"": [
                        {
                            ""name"": ""Egg Sandwich"",
                            ""ingredients"": [
                                {
                                    ""id"": 1,
                                    ""name"": ""Egg"",
                                    ""quantity"": -1
                                },
                                {
                                    ""id"": 3,
                                    ""name"": ""Bread Slice"",
                                    ""quantity"": -2
                                }
                            ]
                        },
                        {
                            ""name"": ""Cheese Sandwich"",
                            ""ingredients"": [
                                {
                                    ""id"": 4,
                                    ""name"": ""Cheese Slice"",
                                    ""quantity"": -2
                                },
                                {
                                    ""id"": 3,
                                    ""name"": ""Bread Slice"",
                                    ""quantity"": -2
                                }
                            ]
                        },
                        {
                            ""name"": ""BEC Sandwich"",
                            ""ingredients"": [
                                {
                                    ""id"": 2,
                                    ""name"": ""Bacon"",
                                    ""quantity"": -3
                                },
                                {
                                    ""id"": 1,
                                    ""name"": ""Egg"",
                                    ""quantity"": -1
                                },
                                {
                                    ""id"": 4,
                                    ""name"": ""Cheese Slice"",
                                    ""quantity"": -1
                                },
                                {
                                    ""id"": 3,
                                    ""name"": ""Bread Slice"",
                                    ""quantity"": -2
                                }
                            ]
                        }
                    ]
                }";
    }
}
