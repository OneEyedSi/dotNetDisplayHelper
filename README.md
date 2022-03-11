# DisplayHelper
DisplayHelper is a small .NET library designed to simplify the display of objects and text.  Currently it can be used as-is with console applications and Windows Forms applications.  However, it is relatively straight-forward to extend the base DisplayHelper class for use in other applications.

The DisplayHelper library targets **.NET Framework 2.0** to make it as broadly usable as possible.

## Getting Started
The repository contains a Visual Studio 2017 solution.  The solution contains two projects:

1. **DisplayHelper**: The DisplayHelper library.  This is made up of a base DisplayHelper class plus derived classes for displaying text in Console applications, in Windows Forms TextBoxes, and in Windows Forms RichTextBoxes.

2. **DisplayHelperDemo**: A console and Windows Forms application that demonstrates the functionality provided by the DisplayHelper library.

The **MainMenu** class in the **DisplayHelperDemo** project demonstrates how to use the **ConsoleDisplayHelper** class to display text in a **Console application**.  The **ObjectViewerForm** demonstrates how to use the **TextBoxDisplayHelper** class and **RichTextBoxDisplayHelper** class to display text in **Windows Forms TextBoxes** and **RichTextBoxes**, respectively.

## Functionality Provided by DisplayHelper

### Displaying Objects
The DisplayHelper uses reflection to display the values of an object's properties and fields.  It will recurse down through the object graph, displaying the values of sub-properties (properties of objects that are properties of other objects), sub-sub-properties, etc.

The DisplayHelper has two mechanisms to prevent endless recursion: 

1. It will not follow direct or indirect circular references (for example, if a parent object has a Child property that has a Parent property pointing back to the original parent object).  Instead it displays a message it has encountered a circular reference and skips the property involved, continuing to display details of other properties;

2. It tracks how deep the recursion gets.  It currently has a hard-coded maximum recursion depth of 5: If the same type of object appears on 5 different levels within the object graph the DisplayHelper will terminate its walk through the graph.  In that case a message will be displayed to inform the user the DisplayHelper is terminating without having viewed the whole object graph, and the reason for the early termination.

In addition to recursing down through an object's properties, the DisplayHelper can display the properties of lists or arrays of objects.

The DisplayHelper will display messages if an object or property is null, or if a collection has no elements.

Static properties and fields of an object are not displayed.  This is because static members are not ordinarily accessible from an object; normally they are accessed from the class, not from an instance of that type.

### Displaying Exceptions
The DisplayHelper will display the type of an exception and the exception message.  It will recurse down through any inner exceptions, and inner-inner exceptions, etc, displaying the details of each.

### Displaying Data Tables
The DisplayHelper will display the values in every cell of every row of a specified data table.  It can handle empty data tables and data tables that are set to null.

### Displaying XML and JSON Text
The DisplayHelper will add line breaks and indents when displaying XML and JSON text to make them more readable.

### Displaying Text
The DisplayHelper has several helper methods for displaying text.  These are most useful in Console applications, although they can be used in Windows Forms applications as well:

* Display indented text, with the indent width specified;

* Display headed text, with the text in the form `Header: My text.`  The indent level of the text can be specified;

* Display numbered text, of the form: `2) My text.`  The indent level of the text can be specified;

* Display titles and sub-titles, where the text is underlined.  The only difference between a title and sub-title is the style of the underline.

### Get Display-friendly Text
The DisplayHelper includes a helper method for converting text to a display-friendly format.  Specifically, if the string is null, empty or blank it will be converted to a user-friendly string, such as "[NULL]", "[EMPTY STRING]" or "[BLANK STRING]".

### Get Exception Details
In addition to displaying exception details the DisplayHelper includes a helper method that returns exception details as a string.  This may be useful for logging.  Similar to displaying the exception details, the string returned from the helper method includes the type of the exception and the exception message.  It also recursively includes the details of any inner exceptions and inner-inner exceptions, etc.

## NuGet Package
If required, [install NuGet](http://docs.nuget.org/docs/start-here/installing-nuget) for Visual Studio. Then, in Visual Studio, open the solution to add the package to and use the Package Manager Console to install [DisplayHelper](https://www.nuget.org/packages/DisplayHelper/):

    PM> Install-Package DisplayHelper
