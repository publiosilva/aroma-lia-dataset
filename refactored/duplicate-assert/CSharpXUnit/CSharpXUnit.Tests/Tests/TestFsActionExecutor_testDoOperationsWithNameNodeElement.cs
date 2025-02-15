public class TestFsActionExecutor
{
    [Fact]
    public void TestDoOperationsWithNameNodeElement1()
    {
        var ae = new FsActionExecutor();
        var fs = GetFileSystem();

        var mkdir = new Path(GetFsTestCaseDir(), "mkdir");
        var mkdirX = new Path(mkdir.ToUri().GetPath());
        var delete = new Path(GetFsTestCaseDir(), "delete");
        var deleteX = new Path(delete.ToUri().GetPath());
        fs.Mkdirs(delete);
        var source = new Path(GetFsTestCaseDir(), "source");
        var sourceX = new Path(source.ToUri().GetPath());
        fs.Mkdirs(source);
        var target = new Path(new Path(GetFsTestCaseDir(), "target").ToUri().GetPath());
        var chmod1 = new Path(GetFsTestCaseDir(), "chmod1");
        var chmod1X = new Path(chmod1.ToUri().GetPath());
        fs.Mkdirs(chmod1);
        var child1 = new Path(chmod1, "child1");
        fs.Mkdirs(child1);
        var chmod2 = new Path(GetFsTestCaseDir(), "chmod2");
        var chmod2X = new Path(chmod2.ToUri().GetPath());
        fs.Mkdirs(chmod2);
        var child2 = new Path(chmod2, "child2");
        fs.Mkdirs(child2);
        var newFile1 = new Path(mkdir + "newFile1");
        var newFile1X = new Path(newFile1.ToUri().GetPath());
        var newFile2 = new Path(mkdir + "newFile2");
        var newFile2X = new Path(newFile2.ToUri().GetPath());
        fs.CreateNewFile(newFile1);
        var chmod3 = new Path(GetFsTestCaseDir(), "chmod3");
        var chmod3X = new Path(chmod3.ToUri().GetPath());
        fs.Mkdirs(chmod3);
        var child3 = new Path(chmod3, "child3");
        fs.Mkdirs(child3);
        var grandchild3 = new Path(child3, "grandchild1");
        fs.Mkdirs(grandchild3);

        var str = string.Format("<root><name-node>{0}</name-node>" +
                "<mkdir path='{1}'/>" +
                "<delete path='{2}'/>" +
                "<move source='{3}' target='{4}'/>" +
                "<chmod path='{5}' permissions='-rwxrwxrwx'/>" +
                "<chmod path='{6}' permissions='-rwxrwx---' dir-files='false'/>" +
                "<touchz path='{7}'/>" +
                "<touchz path='{8}'/>" +
                "<chmod path='{9}' permissions='-rwxrwx---'> <recursive/> </chmod>" +
                "</root>", GetNameNodeUri(), mkdirX, deleteX, sourceX, target, chmod1X, chmod2X, newFile1X, newFile2X, chmod3X);

        var xml = XmlUtils.ParseXml(str);

        ae.DoOperations(CreateContext("<fs/>"), xml);

        Assert.True(fs.Exists(mkdir));
        Assert.False(fs.Exists(delete));
        Assert.False(fs.Exists(source));
        Assert.True(fs.Exists(target));
        Assert.True(fs.Exists(newFile1));
        Assert.True(fs.Exists(newFile2));

        Assert.Equal("rwxrwxrwx", fs.GetFileStatus(chmod1).GetPermission().ToString());
        Assert.NotSame("rwxrwxrwx", fs.GetFileStatus(child1).GetPermission().ToString());
        Assert.Equal("rwxrwx---", fs.GetFileStatus(chmod2).GetPermission().ToString());
        Assert.NotSame("rwxrwx---", fs.GetFileStatus(child2).GetPermission().ToString());

        Assert.Equal("rwxrwx---", fs.GetFileStatus(child3).GetPermission().ToString());
        Assert.Equal("rwxrwx---", fs.GetFileStatus(grandchild3).GetPermission().ToString());
    }

    [Fact]
    public void TestDoOperationsWithNameNodeElement2()
    {
        var ae = new FsActionExecutor();
        var fs = GetFileSystem();

        var mkdir = new Path(GetFsTestCaseDir(), "mkdir");
        var mkdirX = new Path(mkdir.ToUri().GetPath());
        var delete = new Path(GetFsTestCaseDir(), "delete");
        var deleteX = new Path(delete.ToUri().GetPath());
        fs.Mkdirs(delete);
        var source = new Path(GetFsTestCaseDir(), "source");
        var sourceX = new Path(source.ToUri().GetPath());
        fs.Mkdirs(source);
        var target = new Path(new Path(GetFsTestCaseDir(), "target").ToUri().GetPath());
        var chmod1 = new Path(GetFsTestCaseDir(), "chmod1");
        var chmod1X = new Path(chmod1.ToUri().GetPath());
        fs.Mkdirs(chmod1);
        var child1 = new Path(chmod1, "child1");
        fs.Mkdirs(child1);
        var chmod2 = new Path(GetFsTestCaseDir(), "chmod2");
        var chmod2X = new Path(chmod2.ToUri().GetPath());
        fs.Mkdirs(chmod2);
        var child2 = new Path(chmod2, "child2");
        fs.Mkdirs(child2);
        var newFile1 = new Path(mkdir + "newFile1");
        var newFile1X = new Path(newFile1.ToUri().GetPath());
        var newFile2 = new Path(mkdir + "newFile2");
        var newFile2X = new Path(newFile2.ToUri().GetPath());
        fs.CreateNewFile(newFile1);
        var chmod3 = new Path(GetFsTestCaseDir(), "chmod3");
        var chmod3X = new Path(chmod3.ToUri().GetPath());
        fs.Mkdirs(chmod3);
        var child3 = new Path(chmod3, "child3");
        fs.Mkdirs(child3);
        var grandchild3 = new Path(child3, "grandchild1");
        fs.Mkdirs(grandchild3);

        var str = string.Format("<root><name-node>{0}</name-node>" +
                "<mkdir path='{1}'/>" +
                "<delete path='{2}'/>" +
                "<move source='{3}' target='{4}'/>" +
                "<chmod path='{5}' permissions='-rwxrwxrwx'/>" +
                "<chmod path='{6}' permissions='-rwxrwx---' dir-files='false'/>" +
                "<touchz path='{7}'/>" +
                "<touchz path='{8}'/>" +
                "<chmod path='{9}' permissions='-rwxrwx---'> <recursive/> </chmod>" +
                "</root>", GetNameNodeUri(), mkdirX, deleteX, sourceX, target, chmod1X, chmod2X, newFile1X, newFile2X, chmod3X);

        var xml = XmlUtils.ParseXml(str);

        ae.DoOperations(CreateContext("<fs/>"), xml);

        Assert.True(fs.Exists(mkdir));
        Assert.False(fs.Exists(delete));
        Assert.False(fs.Exists(source));
        Assert.True(fs.Exists(target));
        Assert.True(fs.Exists(newFile1));
        Assert.True(fs.Exists(newFile2));

        Assert.Equal("rwxrwxrwx", fs.GetFileStatus(chmod1).GetPermission().ToString());
        Assert.NotSame("rwxrwxrwx", fs.GetFileStatus(child1).GetPermission().ToString());
        Assert.Equal("rwxrwx---", fs.GetFileStatus(chmod2).GetPermission().ToString());
        Assert.NotSame("rwxrwx---", fs.GetFileStatus(child2).GetPermission().ToString());

        Assert.Equal("rwxrwx---", fs.GetFileStatus(child3).GetPermission().ToString());
        Assert.Equal("rwxrwx---", fs.GetFileStatus(grandchild3).GetPermission().ToString());
    }
}
