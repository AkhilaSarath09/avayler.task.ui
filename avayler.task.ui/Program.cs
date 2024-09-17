
using avayler.task.lib;


Console.WriteLine("Enter the Elementery Word");
var input = Console.ReadLine();
var result = ElementWordsFinder.ElementalForms(input);

Console.WriteLine(result);
Console.ReadLine();
