import static org.junit.jupiter.api.Assertions.assertEquals;

import org.junit.jupiter.api.Test;

public class DetailAstImplTest extends AbstractModuleTestSupport {

  @Test
    public void testClearChildCountCache() {
        final DetailAstImpl parent = new DetailAstImpl();
        final DetailAstImpl child = new DetailAstImpl();
        parent.setFirstChild(child);

        final List<Consumer<DetailAstImpl>> clearChildCountCacheMethods = Arrays.asList(
                child::setNextSibling,
                child::addPreviousSibling,
                child::addNextSibling
        );

        for (Consumer<DetailAstImpl> method : clearChildCountCacheMethods) {
            final int startCount = parent.getChildCount();
            method.accept(null);
            final int intermediateCount = Whitebox.getInternalState(parent, "childCount");
            final int finishCount = parent.getChildCount();
            assertEquals(startCount, finishCount, "Child count has changed");
            assertEquals(Integer.MIN_VALUE, intermediateCount, "Invalid child count");
        }

        final int startCount = child.getChildCount();
        child.addChild(null);
        final int intermediateCount = Whitebox.getInternalState(child, "childCount");
        final int finishCount = child.getChildCount();
        assertEquals(startCount, finishCount, "Child count has changed");
        assertEquals(Integer.MIN_VALUE, intermediateCount, "Invalid child count");
    }
}
