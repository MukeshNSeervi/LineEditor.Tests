using System.Text;
using LineEditor;
public class LineEditorTests
{
    [Fact]
    public void Main_NoFileNameProvided_ShouldPromptForFileName()
    {
        // Arrange
        var stringWriter = new StringWriter();
        Console.SetOut(stringWriter);

        string[] args = Array.Empty<string>();

        // Act
        LineEditor.LineEditor.Main(args);

        // Assert
        var output = stringWriter.ToString().Trim();
        Assert.Equal("Please Pass FileName", output);
    }

    [Fact]
    public void Main_InvalidFileNAmeProvided_ShouldPromptForFileName()
    {
        //Arrange
        var stringWriter = new StringWriter();
        Console.SetOut(stringWriter);

        string[] args = ["TestFile.txt"];

        // Act
        LineEditor.LineEditor.Main(args);

        // Assert
        var output = stringWriter.ToString().Trim();
        Assert.Equal("File not found", output);
    }

    [Fact]
    public void List_ValidFile_ShouldHaveData()
    {
        //Arrange
        string[] args = ["d:\\csharp\\OpenText\\LineEditor2\\TestFile.txt","list"];
        string[] fileData = File.ReadAllLines(args[0]);
        StringBuilder ExpectedOutPut = new();
        int i=0;
        foreach(string eachLine in fileData)
        {
            ExpectedOutPut.Append($"{++i} : {eachLine}").AppendLine();
        }
        var stringWriter = new StringWriter();
        Console.SetOut(stringWriter);

        //Act
        LineEditor.LineEditor.Main(args);

        //Assert
        Assert.Equal(stringWriter.ToString(),ExpectedOutPut.ToString());
    }

    [Fact]
    public void Insert_ValidIndex_VerifyNewDataByListing()
    {
        //Arrange
        string[] args = ["d:\\csharp\\OpenText\\LineEditor2\\TestFile.txt","ins 1 FirstLine","list"];
        string[] fileData = File.ReadAllLines(args[0]);
        StringBuilder ExpectedOutPut = new();
        ExpectedOutPut.Append($"{1} : FirstLine").AppendLine();
        int i=1;
        foreach(string eachLine in fileData)
        {
            ExpectedOutPut.Append($"{++i} : {eachLine}").AppendLine();
        }
        var stringWriter = new StringWriter();
        Console.SetOut(stringWriter);

        //Act
        LineEditor.LineEditor.Main(args);

        //Assert
        Assert.Equal(stringWriter.ToString(),ExpectedOutPut.ToString());
    }

    [Fact]
    public void Delete_ValidIndex_FileDataLengthDecreasedBtOne()
    {
        //Arrange
        string[] args = ["d:\\csharp\\OpenText\\LineEditor2\\TestFile.txt","del 1","list"];
        string[] fileData = File.ReadAllLines(args[0]);
        StringBuilder ExpectedOutPut = new();
        int i=0;
        fileData = fileData.Skip(1).ToArray();
        foreach(string eachLine in fileData)
        {
            ExpectedOutPut.Append($"{++i} : {eachLine}").AppendLine();
        }
        var stringWriter = new StringWriter();
        Console.SetOut(stringWriter);

        //Act
        LineEditor.LineEditor.Main(args);

        //Assert
        Assert.Equal(stringWriter.ToString(),ExpectedOutPut.ToString());

    }

    [Fact]
    public void delete_Save_NewFileReadShouldHaveMoreData()
    {
        //Arrange
        string[] args = ["d:\\csharp\\OpenText\\LineEditor2\\TestFile.txt","del 1","save"];
        string[] fileData = File.ReadAllLines(args[0]);
        StringBuilder ExpectedOutPut = new();
        int i=0;
        fileData = fileData.Skip(1).ToArray();
        foreach(string eachLine in fileData)
        {
            ExpectedOutPut.Append($"{++i} : {eachLine}").AppendLine();
        }
        var stringWriter = new StringWriter();
        Console.SetOut(stringWriter);

        //Act
        LineEditor.LineEditor.Main(args);
        i = 0;
        StringBuilder actualOutput = new();
        fileData = File.ReadAllLines(args[0]);
        foreach(string eachLine in fileData)
        {
            actualOutput.Append($"{++i} : {eachLine}").AppendLine();
        }
        //Assert
        Assert.Equal(actualOutput.ToString(),ExpectedOutPut.ToString());
    }

    [Fact]
    public void Insert_Quit_NewFileReadShouldHaveOriginalReadSize()
    {
        //Arrange
        string[] args = ["d:\\csharp\\OpenText\\LineEditor2\\TestFile.txt","ins 1 Readline","quit"];
        string[] fileData = File.ReadAllLines(args[0]);
        StringBuilder ExpectedOutPut = new();
        int i=0;
       
        foreach(string eachLine in fileData)
        {
            ExpectedOutPut.Append($"{++i} : {eachLine}").AppendLine();
        }
        var stringWriter = new StringWriter();
        Console.SetOut(stringWriter);

        //Act
        LineEditor.LineEditor.Main(args);
        i = 0;
        StringBuilder actualOutput = new();
        fileData = File.ReadAllLines(args[0]);
        foreach(string eachLine in fileData)
        {
            actualOutput.Append($"{++i} : {eachLine}").AppendLine();
        }
        //Assert
        Assert.Equal(actualOutput.ToString(),ExpectedOutPut.ToString());
    }
}