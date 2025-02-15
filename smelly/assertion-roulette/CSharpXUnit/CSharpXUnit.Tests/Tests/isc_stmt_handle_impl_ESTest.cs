[Collection("EvoRunner")]
public class isc_stmt_handle_impl_ESTest : isc_stmt_handle_impl_ESTest_scaffolding
{
    [Fact(Timeout = 4000)]
    public void test00()
    {
        var isc_stmt_handle_impl0 = new isc_stmt_handle_impl();
        isc_stmt_handle_impl0.ensureCapacity(1010);
        isc_stmt_handle_impl0.ensureCapacity(0);
        Assert.False(isc_stmt_handle_impl0.isAllRowsFetched());
        Assert.Equal(0, isc_stmt_handle_impl0.size());
        Assert.False(isc_stmt_handle_impl0.isSingletonResult());
    }
}
