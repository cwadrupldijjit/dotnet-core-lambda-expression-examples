using System;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;

namespace CoreLambdaExpressions
{
    class Program
    {
        private static List<Person> people = new List<Person>
        {
            new Person { Name = "Gerald", ID = 1 },
            new Person { Name = "Ivan", ID = 2 },
            new Person { Name = "Danika", ID = 3 },
            new Person { Name = "Hettie", ID = 4 },
        };
        
        static void Main(string[] args)
        {
            var names = people.Select(
                // An example of a lambda expression with only one parameter
                person => {
                    return person.Name;
                }
                //// The parentheses are optional for a single parameter unless type is included (type is not generally required)
                // (Person person) => {
                //     return person.Name;
                // }
                // 
                //// The function body is also optional so long as you're wanting to return the result of the expression on the right
                // person => person.Name
            );

            Debug.WriteLine(String.Join(", ", names));

            AsyncCallbackExample(
                // An example of a lambda expression with no parameters
                () => {
                    Debug.WriteLine("Lambda with no parameters");
                }
            )
            .Wait();

            CallbackMultipleParams(
                // An example of a lambda expression that has multiple parameters
                (string part1, string part2, string part3) => {
                    var result = $"{part1} {part2} {part3}";

                    Debug.WriteLine(result);
                }
            );

            CallbackMultipleParams(
                // A strength of lambda functions is defining different behaviors based off of results and with the ability to have more than one output (though you can also use `out` for that kind of thing)
                (string part1, string part2, string part3) => {
                    var result = $"{part3} {part2} {part1}";

                    Debug.WriteLine(result);
                }
            );
        }

        static Task AsyncCallbackExample(Action callback)
        {
            var task = new Task(callback);
            task.Start();
            return task;
        }

        static void CallbackMultipleParams(Action<string, string, string> callback)
        {
            callback("How", "are", "you?");
        }
    }

    class Person
    {
        public string Name { get; set; }
        public int ID { get; set; }
    }
}
