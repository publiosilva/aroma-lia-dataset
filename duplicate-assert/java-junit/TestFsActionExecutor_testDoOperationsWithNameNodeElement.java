public class TestFsActionExecutor extends ActionExecutorTestCase {
    public void testDoOperationsWithNameNodeElement() throws Exception {
        FsActionExecutor ae = new FsActionExecutor();
        FileSystem fs = getFileSystem();

        Path mkdir = new Path(getFsTestCaseDir(), "mkdir");
        Path mkdirX = new Path(mkdir.toUri().getPath());
        Path delete = new Path(getFsTestCaseDir(), "delete");
        Path deleteX = new Path(delete.toUri().getPath());
        fs.mkdirs(delete);
        Path source = new Path(getFsTestCaseDir(), "source");
        Path sourceX = new Path(source.toUri().getPath());
        fs.mkdirs(source);
        Path target = new Path(new Path(getFsTestCaseDir(), "target").toUri().getPath());
        Path chmod1 = new Path(getFsTestCaseDir(), "chmod1");
        Path chmod1X = new Path(chmod1.toUri().getPath());
        fs.mkdirs(chmod1);
        Path child1 = new Path(chmod1, "child1");
        fs.mkdirs(child1);
        Path chmod2 = new Path(getFsTestCaseDir(), "chmod2");
        Path chmod2X = new Path(chmod2.toUri().getPath());
        fs.mkdirs(chmod2);
        Path child2 = new Path(chmod2, "child2");
        fs.mkdirs(child2);
        Path newFile1 = new Path(mkdir + "newFile1");
        Path newFile1X = new Path(newFile1.toUri().getPath());
        Path newFile2 = new Path(mkdir + "newFile2");
        Path newFile2X = new Path(newFile2.toUri().getPath());
        fs.createNewFile(newFile1);
        Path chmod3 = new Path(getFsTestCaseDir(), "chmod3");
        Path chmod3X = new Path(chmod3.toUri().getPath());
        fs.mkdirs(chmod3);
        Path child3 = new Path(chmod3, "child3");
        fs.mkdirs(child3);
        Path grandchild3 = new Path(child3, "grandchild1");
        fs.mkdirs(grandchild3);

        String str = MessageFormat.format("<root><name-node>{0}</name-node>" +
                "<mkdir path=''{1}''/>" +
                "<delete path=''{2}''/>" +
                "<move source=''{3}'' target=''{4}''/>" +
                "<chmod path=''{5}'' permissions=''-rwxrwxrwx''/>" +
                "<chmod path=''{6}'' permissions=''-rwxrwx---'' dir-files=''false''/>" +
                "<touchz path=''{7}''/>" +
                "<touchz path=''{8}''/>" +
                "<chmod path=''{9}'' permissions=''-rwxrwx---''> <recursive/> </chmod>" +
                "</root>", getNameNodeUri(), mkdirX, deleteX, sourceX, target, chmod1X, chmod2X, newFile1X, newFile2X, chmod3X);

        Element xml = XmlUtils.parseXml(str);

        ae.doOperations(createContext("<fs/>"), xml);

        assertTrue(fs.exists(mkdir));
        assertFalse(fs.exists(delete));
        assertFalse(fs.exists(source));
        assertTrue(fs.exists(target));
        assertTrue(fs.exists(newFile1));
        assertTrue(fs.exists(newFile2));

        assertEquals("rwxrwxrwx", fs.getFileStatus(chmod1).getPermission().toString());
        assertNotSame("rwxrwxrwx", fs.getFileStatus(child1).getPermission().toString());
        assertEquals("rwxrwx---", fs.getFileStatus(chmod2).getPermission().toString());
        assertNotSame("rwxrwx---", fs.getFileStatus(child2).getPermission().toString());

        assertEquals("rwxrwx---", fs.getFileStatus(child3).getPermission().toString());
        assertEquals("rwxrwx---", fs.getFileStatus(grandchild3).getPermission().toString());

        assertEquals("rwxrwx---", fs.getFileStatus(child3).getPermission().toString());
        assertEquals("rwxrwx---", fs.getFileStatus(grandchild3).getPermission().toString());
    }
}
