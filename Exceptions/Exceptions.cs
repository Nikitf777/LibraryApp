namespace LibraryApp.Exceptions;

public class NotFoundException(string message) : Exception(message);
public class DependentRecordsException(string message) : Exception(message);
