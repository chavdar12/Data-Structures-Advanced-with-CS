namespace AVLTests;

public class AVLTests
{
    [Test]
    public void TraverseInOrder_AfterSingleInsert()
    {
        // Arrange
        var avl = new AVL<int>();
        avl.Insert(1);

        // Act
        var nodes = new List<int>();
        avl.EachInOrder(nodes.Add);

        // Assert
        var expectedNodes = new int[] { 1 };
        CollectionAssert.AreEqual(expectedNodes, nodes);
    }

    [Test]
    public void TraverseInOrder_AfterMultipleInserts()
    {
        // Arrange
        var avl = new AVL<int>();
        avl.Insert(2);
        avl.Insert(1);
        avl.Insert(3);

        // Act
        var nodes = new List<int>();
        avl.EachInOrder(nodes.Add);

        // Assert
        var expectedNodes = new int[] { 1, 2, 3 };
        CollectionAssert.AreEqual(expectedNodes, nodes);
    }

    [Test]
    public void Contains_ExistingElement_ShouldReturnTrue()
    {
        // Arrange
        var avl = new AVL<int>();
        avl.Insert(2);
        avl.Insert(1);
        avl.Insert(3);

        // Act
        // Assert
        Assert.IsTrue(avl.Contains(1));
        Assert.IsTrue(avl.Contains(2));
        Assert.IsTrue(avl.Contains(3));
    }

    [Test]
    public void Contains_NonExistingElement_ShouldReturnFalse()
    {
        // Arrange
        var avl = new AVL<int>();
        avl.Insert(2);
        avl.Insert(1);
        avl.Insert(3);

        // Act
        var contains = avl.Contains(5);

        // Assert
        Assert.IsFalse(contains);
    }

    [Test]
    public void Height_RootRight()
    {
        // Arrange
        var avl = new AVL<int>();
        avl.Insert(1);
        avl.Insert(2);

        // Act
        // Assert
        Assert.AreEqual(2, avl.Root.Height);
        Assert.AreEqual(1, avl.Root.Right.Height);
    }

    [Test]
    public void Height_RootLeft()
    {
        // Arrange
        var avl = new AVL<int>();
        avl.Insert(2);
        avl.Insert(1);

        // Act
        // Assert
        Assert.AreEqual(2, avl.Root.Height);
        Assert.AreEqual(1, avl.Root.Left.Height);
    }

    [Test]
    public void Height_RootLeftRight()
    {
        // Arrange
        var avl = new AVL<int>();
        avl.Insert(2);
        avl.Insert(1);
        avl.Insert(3);

        // Act
        // Assert
        Assert.AreEqual(2, avl.Root.Height);
        Assert.AreEqual(1, avl.Root.Left.Height);
        Assert.AreEqual(1, avl.Root.Right.Height);
    }

    [Test]
    public void Rebalance_RootShouldHaveHeightTwo()
    {
        // Arrange
        var avl = new AVL<int>();
        avl.Insert(1);
        avl.Insert(2);
        avl.Insert(3);

        // Assert
        Assert.AreEqual(2, avl.Root.Height);
    }

    [Test]
    public void Rebalance_TestHeightOneNodes()
    {
        // Arrange
        var avl = new AVL<int>();
        for (var i = 1; i < 10; i++) avl.Insert(i);

        // Assert
        Assert.AreEqual(1, avl.Root.Left.Left.Height); // 1
        Assert.AreEqual(1, avl.Root.Left.Right.Height); // 3
        Assert.AreEqual(1, avl.Root.Right.Left.Height); // 5
        Assert.AreEqual(1, avl.Root.Right.Right.Left.Height); // 7
        Assert.AreEqual(1, avl.Root.Right.Right.Right.Height); // 9
    }

    [Test]
    public void Rebalance_TestHeightTwoNodes()
    {
        // Arrange
        var avl = new AVL<int>();
        for (var i = 1; i < 10; i++) avl.Insert(i);

        // Assert
        Assert.AreEqual(2, avl.Root.Left.Height); // 2
        Assert.AreEqual(2, avl.Root.Right.Right.Height); // 8
    }

    [Test]
    public void Rebalance_TestHeightThreeNodes()
    {
        // Arrange
        var avl = new AVL<int>();
        for (var i = 1; i < 10; i++) avl.Insert(i);

        // Assert
        Assert.AreEqual(3, avl.Root.Right.Height); // 6
    }

    [Test]
    public void Rebalance_TestHeightFourNodes()
    {
        // Arrange
        var avl = new AVL<int>();
        for (var i = 1; i < 10; i++) avl.Insert(i);

        // Assert
        Assert.AreEqual(4, avl.Root.Height); // 4
    }

    [Test]
    public void Rebalance_SingleRight()
    {
        // Arrange
        var avl = new AVL<int>();

        // Act
        avl.Insert(3);
        avl.Insert(2);
        avl.Insert(1);

        // Assert
        Assert.AreEqual(2, avl.Root.Value);
    }

    [Test]
    public void Rebalance_SingleLeft()
    {
        // Arrange
        var avl = new AVL<int>();

        // Act
        avl.Insert(1);
        avl.Insert(2);
        avl.Insert(3);

        // Assert
        Assert.AreEqual(2, avl.Root.Value);
    }

    [Test]
    public void Rebalance_DoubleRight()
    {
        // Arrange
        var avl = new AVL<int>();

        // Act
        avl.Insert(5);
        avl.Insert(2);
        avl.Insert(4);

        // Assert
        Assert.AreEqual(4, avl.Root.Value);
        Assert.AreEqual(2, avl.Root.Height);
        Assert.AreEqual(1, avl.Root.Left.Height);
        Assert.AreEqual(1, avl.Root.Right.Height);
    }

    [Test]
    public void Rebalance_DoubleLeft()
    {
        // Arrange
        var avl = new AVL<int>();

        // Act
        avl.Insert(5);
        avl.Insert(7);
        avl.Insert(6);

        // Assert
        Assert.AreEqual(6, avl.Root.Value);
        Assert.AreEqual(2, avl.Root.Height);
        Assert.AreEqual(1, avl.Root.Left.Height);
        Assert.AreEqual(1, avl.Root.Right.Height);
    }

    [Test]
    [Timeout(400)]
    public void Performance_Insert_Contains()
    {
        var avl = new AVL<int>();

        for (var i = 0; i < 100000; i++) avl.Insert(i);

        for (var i = 0; i < 100000; i++) Assert.IsTrue(avl.Contains(i));
    }

    [Test]
    public void Height_DeleteRoot()
    {
        // Arrange
        var avl = new AVL<int>();
        for (var i = 1; i < 10; i++) avl.Insert(i);

        avl.Delete(4);

        var root = avl.Root;

        // Assert
        Assert.AreEqual(5, root.Value);
        Assert.AreEqual(4, root.Height);

        // Nodes of height 1
        Assert.AreEqual(1, root.Left.Left.Height); // 1
        Assert.AreEqual(1, root.Left.Right.Height); // 3
        Assert.AreEqual(1, root.Right.Left.Right.Height); // 7
        Assert.AreEqual(1, root.Right.Right.Height); // 9

        // Nodes of height 2
        Assert.AreEqual(2, root.Left.Height); // 2
        Assert.AreEqual(2, root.Right.Left.Height); //6

        // Nodes of height 3
        Assert.AreEqual(3, root.Right.Height); // 8

        // Nodes of height 4
        Assert.AreEqual(4, root.Height); // 5
    }

    [Test]
    public void Height_DeleteMultiple()
    {
        // Arrange
        var avl = new AVL<int>();
        for (var i = 1; i < 10; i++) avl.Insert(i);

        avl.Delete(4);
        avl.Delete(2);
        avl.Delete(1);

        var root = avl.Root;

        // Assert
        Assert.AreEqual(6, root.Value);
        Assert.AreEqual(3, root.Height);

        // Nodes of height 1
        Assert.AreEqual(3, root.Left.Left.Value);
        Assert.AreEqual(1, root.Left.Left.Height); // 3

        Assert.AreEqual(7, root.Right.Left.Value);
        Assert.AreEqual(1, root.Right.Left.Height); // 7

        Assert.AreEqual(9, root.Right.Right.Value);
        Assert.AreEqual(1, root.Right.Right.Height); // 9

        // Nodes of height 2
        Assert.AreEqual(5, root.Left.Value);
        Assert.AreEqual(2, root.Left.Height); // 5

        Assert.AreEqual(8, root.Right.Value);
        Assert.AreEqual(2, root.Right.Height); // 8
    }

    [Test]
    public void DeleteMin_AfterMultipleInserts()
    {
        // Arrange
        var avl = new AVL<int>();
        avl.Insert(2);
        avl.Insert(1);
        avl.Insert(3);

        // Act
        avl.DeleteMin();
        var nodes = new List<int>();
        avl.EachInOrder(nodes.Add);

        // Assert
        var expectedNodes = new int[] { 2, 3 };
        CollectionAssert.AreEqual(expectedNodes, nodes);
    }

    [Test]
    public void DeleteMin_Empty_Tree_Should_Work_Correctly()
    {
        // Arrange
        var avl = new AVL<int>();

        // Act
        avl.DeleteMin();
        var nodes = new List<int>();
        avl.EachInOrder(nodes.Add);

        // Assert
        var expectedNodes = new int[] { };
        CollectionAssert.AreEqual(expectedNodes, nodes);
    }

    [Test]
    public void DeleteMin_One_Element()
    {
        // Arrange
        var avl = new AVL<int>();
        avl.Insert(1);

        // Act
        avl.DeleteMin();
        var nodes = new List<int>();
        avl.EachInOrder(nodes.Add);

        // Assert
        var expectedNodes = new int[] { };
        CollectionAssert.AreEqual(expectedNodes, nodes);
    }

    [Test]
    public void Delete_LeftLeaf()
    {
        var avl = new AVL<int>();

        avl.Insert(5);
        avl.Insert(3);
        avl.Insert(1);
        avl.Insert(4);
        avl.Insert(8);
        avl.Insert(9);

        // Act
        avl.Delete(1);
        var result = new List<int>();
        avl.EachInOrder(result.Add);

        // Assert
        var expectedNodes = new int[] { 3, 4, 5, 8, 9 };
        CollectionAssert.AreEqual(expectedNodes, result.ToArray());
    }

    [Test]
    public void Delete_RightLeaf()
    {
        var avl = new AVL<int>();

        avl.Insert(5);
        avl.Insert(3);
        avl.Insert(1);
        avl.Insert(4);
        avl.Insert(8);
        avl.Insert(9);

        // Act
        avl.Delete(4);
        var result = new List<int>();
        avl.EachInOrder(result.Add);

        // Assert
        var expectedNodes = new int[] { 1, 3, 5, 8, 9 };
        CollectionAssert.AreEqual(expectedNodes, result.ToArray());
    }

    [Test]
    public void Delete_NodeWithRightChild()
    {
        var avl = new AVL<int>();

        avl.Insert(5);
        avl.Insert(3);
        avl.Insert(1);
        avl.Insert(4);
        avl.Insert(8);
        avl.Insert(9);

        // Act
        avl.Delete(8);
        var result = new List<int>();
        avl.EachInOrder(result.Add);

        // Assert
        var expectedNodes = new int[] { 1, 3, 4, 5, 9 };
        CollectionAssert.AreEqual(expectedNodes, result.ToArray());
    }

    [Test]
    public void Delete_NodeWithLeftChild()
    {
        var avl = new AVL<int>();

        avl.Insert(5);
        avl.Insert(3);
        avl.Insert(1);
        avl.Insert(4);
        avl.Insert(8);
        avl.Insert(9);

        // Act
        avl.Delete(4);
        avl.Delete(3);
        var result = new List<int>();
        avl.EachInOrder(result.Add);

        // Assert
        var expectedNodes = new int[] { 1, 5, 8, 9 };
        CollectionAssert.AreEqual(expectedNodes, result.ToArray());
    }

    [Test]
    public void Delete_NodeWithTwoChildren()
    {
        var avl = new AVL<int>();

        avl.Insert(5);
        avl.Insert(3);
        avl.Insert(1);
        avl.Insert(4);
        avl.Insert(8);
        avl.Insert(9);

        // Act
        avl.Delete(3);
        var result = new List<int>();
        avl.EachInOrder(result.Add);

        // Assert
        var expectedNodes = new int[] { 1, 4, 5, 8, 9 };
        CollectionAssert.AreEqual(expectedNodes, result.ToArray());
    }

    [Test]
    public void Delete_Root()
    {
        var avl = new AVL<int>();

        avl.Insert(5);
        avl.Insert(3);
        avl.Insert(1);
        avl.Insert(4);
        avl.Insert(8);
        avl.Insert(9);

        // Act
        avl.Delete(5);
        var result = new List<int>();
        avl.EachInOrder(result.Add);

        // Assert
        var expectedNodes = new int[] { 1, 3, 4, 8, 9 };
        CollectionAssert.AreEqual(expectedNodes, result.ToArray());
    }

    [Test]
    public void Delete_Root_EmptyTree()
    {
        var avl = new AVL<int>();

        // Act
        avl.Delete(5);
        var result = new List<int>();
        avl.EachInOrder(result.Add);

        // Assert
        var expectedNodes = new int[] { };
        CollectionAssert.AreEqual(expectedNodes, result.ToArray());
    }

    [Test]
    public void Delete_Root_SingleElement()
    {
        var avl = new AVL<int>();
        avl.Insert(5);

        // Act
        avl.Delete(5);
        var result = new List<int>();
        avl.EachInOrder(result.Add);

        // Assert
        var expectedNodes = new int[] { };
        CollectionAssert.AreEqual(expectedNodes, result.ToArray());
    }
}