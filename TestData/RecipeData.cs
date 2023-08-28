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
                                    ""item"": ""Egg"",
                                    ""quantity"": 1
                                },
                                {
                                    ""item"": ""Bread Slice"",
                                    ""quantity"": 2
                                }
                            ]
                        },
                        {
                            ""name"": ""Cheese Sandwich"",
                            ""ingredients"": [
                                {
                                    ""item"": ""Cheese Slice"",
                                    ""quantity"": 2
                                },
                                {
                                    ""item"": ""Bread Slice"",
                                    ""quantity"": 2
                                }
                            ]
                        },
                        {
                            ""name"": ""BEC Sandwich"",
                            ""ingredients"": [
                                {
                                    ""item"": ""Bacon"",
                                    ""quantity"": 3
                                },
                                {
                                    ""item"": ""Egg"",
                                    ""quantity"": 1
                                },
                                {
                                    ""item"": ""Cheese Slice"",
                                    ""quantity"": 1
                                },
                                {
                                    ""item"": ""Bread Slice"",
                                    ""quantity"": 2
                                }
                            ]
                        }
                    ]
                }";
    }
}
