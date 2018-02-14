using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data;

public static class TransactionExtension
{
    /// <summary>
    /// DbContext에 대한 트랜젝션을 생성합니다.
    /// savechage()를 호출해도 commit되지 않습니다. 명시적으로 Commit해야 합니다.
    /// 다중 DB에 대한 트랜젝션은 TrasactionScope를 사용하세요.
    /// Isolation Level은 기본 데이터베이스 공급자 설정을 사용합니다.
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public static IDbContextTransaction CreateTransaction(this DbContext context)
    {
        return context.Database.BeginTransaction();
    }

    /// <summary>
    /// IDbConnection에 대한 트랜젝션을 생성합니다.
    /// execute()를 호출해도 commit되지 않습니다. 명시적으로 Commit해야 합니다.
    /// 다중 DB에 대한 트랜젝션은 TrasactionScope를 사용하세요.
    /// Isolation Level의 기본값은 ReadCommitted입니다. block을 방지하려면 ReadUnCommitted를 사용하세요.
    /// </summary>
    /// <param name="connection"></param>
    /// <param name="isolationLev">ReadCommitted</param>
    /// <returns></returns>
    public static IDbTransaction CreateTransaction(this IDbConnection connection, IsolationLevel isolationLev = IsolationLevel.ReadCommitted)
    {
        return connection.BeginTransaction(isolationLev);
    }

    /// <summary>
    /// savechage()를 호출해도 commit되지 않습니다. 명시적으로 Commit해야 합니다.
    /// </summary>
    /// <param name="context"></param>
    public static void Commit(this DbContext context)
    {
        context.Database.CommitTransaction();
    }

    public static void Commit(this IDbTransaction tran)
    {
        tran.Commit();
    }
}
