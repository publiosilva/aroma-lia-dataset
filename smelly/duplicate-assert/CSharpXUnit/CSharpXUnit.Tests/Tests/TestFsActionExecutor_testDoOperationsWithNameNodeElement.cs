public class TestFsActionExecutor : ActionExecutorTestCase
{
    [Fact]
    public void TestDoOperationsWithNameNodeElement()
    {
        FsActionExecutor ae = new FsActionExecutor();
        FileSystem fs = GetFileSystem();

        Path mkdir = new Path(GetFsTestCaseDir(), "mkdir");
        Path mkdirX = new Path(mkdir.ToUri().GetPath());
        Path delete = new Path(GetFsTestCaseDir(), "delete");
        Path deleteX = new Path(delete.ToUri().GetPath());
        fs.Mkdirs(delete);
        Path source = new Path(GetFsTestCaseDir(), "source");
        Path sourceX = new Path(source.ToUri().GetPath());
        fs.Mkdirs(source);
        Path target = new Path(new Path(GetFsTestCaseDir(), "target").ToUri().GetPath());
        Path chmod1 = new Path(GetFsTestCaseDir(), "chmod1");
        Path chmod1X = new Path(chmod1.ToUri().GetPath());
        fs.Mkdirs(chmod1);
        Path child1 = new Path(chmod1, "child1");
        fs.Mkdirs(child1);
        Path chmod2 = new Path(GetFsTestCaseDir(), "chmod2");
        Path chmod2X = new Path(chmod2.ToUri().GetPath());
        fs.Mkdirs(chmod2);
        Path child2 = new Path(chmod2, "child2");
        fs.Mkdirs(child2);
        Path newFile1 = new Path(mkdir + "newFile1");
        Path newFile1X = new Path(newFile1.ToUri().GetPath());
        Path newFile2 = new Path(mkdir + "newFile2");
        Path newFile2X = new Path(newFile2.ToUri().GetPath());
        fs.CreateNewFile(newFile1);
        Path chmod3 = new Path(GetFsTestCaseDir(), "chmod3");
        Path chmod3X = new Path(chmod3.ToUri().GetPath());
        fs.Mkdirs(chmod3);
        Path child3 = new Path(chmod3, "child3");
        fs.Mkdirs(child3);
        Path grandchild3 = new Path(child3, "grandchild1");
        fs.Mkdirs(grandchild3);

        string str = string.Format("<root><name-node>{0}</name-node>" +
            "<mkdir path=''{1}''/>" +
            "<delete path=''{2}''/>" +
            "<move source=''{3}'' target=''{4}''/>" +
            "<chmod path=''{5}'' permissions=''-rwxrwxrwx''/>" +
            "<chmod path=''{6}'' permissions=''-rwxrwx---'' dir-files=''false''/>" +
            "<touchz path=''{7}''/>" +
            "<touchz path=''{8}''/>" +
            "<chmod path=''{9}'' permissions=''-rwxrwx---''> <recursive/> </chmod>" +
            "</root>", GetNameNodeUri(), mkdirX, deleteX, sourceX, target, chmod1X, chmod2X, newFile1X, newFile2X, chmod3X);

        Element xml = XmlUtils.ParseXml(str);

        ae.DoOperations(CreateContext("<fs/>"), xml);

        Assert.True(fs.Exists(mkdir));
        Assert.False(fs.Exists(delete));
        Assert.False(fs.Exists(source));
        Assert.True(fs.Exists(target));
        Assert.True(fs.Exists(newFile1));
        Assert.True(fs.Exists(newFile2));

        Assert.Equal("rwxrwxrwx", fs.GetFileStatus(chmod1).GetPermission().ToString());
        Assert.NotEqual("rwxrwxrwx", fs.GetFileStatus(child1).GetPermission().ToString());
        Assert.Equal("rwxrwx---", fs.GetFileStatus(chmod2).GetPermission().ToString());
        Assert.NotEqual("rwxrwx---", fs.GetFileStatus(child2).GetPermission().ToString());

        Assert.Equal("rwxrwx---", fs.GetFileStatus(child3).GetPermission().ToString());
        Assert.Equal("rwxrwx---", fs.GetFileStatus(grandchild3).GetPermission().ToString());

        Assert.Equal("rwxrwx---", fs.GetFileStatus(child3).GetPermission().ToString());
        Assert.Equal("rwxrwx---", fs.GetFileStatus(grandchild3).GetPermission().ToString());
    }
}
