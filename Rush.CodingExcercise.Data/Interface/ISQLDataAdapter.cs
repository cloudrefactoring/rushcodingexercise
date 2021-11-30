namespace Rush.CodingExercise.Data;

public interface ISQLDataAdapter
{
    Task<int> Create<P>(string storeProcedure, P parameters, string defaultConnection = "RushEnterprises");
    Task<T> Create<T, P>(string storeProcedure, P parameters, string defaultConnection = "RushEnterprises");
    Task<IEnumerable<T>> Query<T, U>(string storeProcedure, U parameters, string defaultConnection = "RushEnterprises");
    Task<T> Update<T, P>(string storeProcedure, P parameters, string defaultConnection = "RushEnterprises");
}