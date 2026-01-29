using RoyaleArena;

namespace RoyaleArenaTests;

public class RoyaleArenaTests
{
    [Test]
    public void Add_SingleElement_ShouldWorkCorrectly()
    {
        IArena ra = new RoyaleArena.RoyaleArena();
        var cd = new BattleCard(5, CardType.SPELL, "joro", 5, 5);
        ra.Add(cd);

        //Assert
        foreach (var bc in ra) Assert.That(cd, Is.EqualTo(bc));
    }

    [Test]
    public void Contains_OnExistingElement_ShouldReturnTrue()
    {
        //Arrange
        IArena ra = new RoyaleArena.RoyaleArena();
        var cd1 = new BattleCard(5, CardType.SPELL, "joro", 6, 5);
        var cd2 = new BattleCard(6, CardType.SPELL, "joro", 7, 5);
        var cd3 = new BattleCard(7, CardType.SPELL, "joro", 8, 5);

        //Act
        ra.Add(cd1);
        ra.Add(cd2);
        ra.Add(cd3);
        //Assert

        Assert.That(ra.Contains(cd1), Is.True);
        Assert.That(ra.Contains(new BattleCard(3, CardType.BUILDING, "ta", 6, 52.2)), Is.False);
        Assert.That(ra.Contains(cd2), Is.True);
        Assert.That(ra.Contains(new BattleCard(0, CardType.RANGED, "b", 7, 5)), Is.False);
    }

    [Test]
    public void Count_Should_IncreaseOnMultiple_Elements()
    {
        //Arrange
        IArena ra = new RoyaleArena.RoyaleArena();
        var cd1 = new BattleCard(5, CardType.SPELL, "joro", 3, 5);
        var cd2 = new BattleCard(6, CardType.SPELL, "joro", 8, 5);
        var cd3 = new BattleCard(7, CardType.SPELL, "joro", 9, 5);

        //Act
        ra.Add(cd1);
        ra.Add(cd2);
        ra.Add(cd3);

        //Assert
        Assert.That(ra.Count, Is.EqualTo(3));
    }

    [Test]
    public void GetById_On_ExistingElement_ShouldWorkCorrectly()
    {
        //Arrange
        IArena ra = new RoyaleArena.RoyaleArena();
        var cd1 = new BattleCard(5, CardType.SPELL, "joro", 10, 5);
        var cd2 = new BattleCard(6, CardType.SPELL, "joro", 10, 5);
        var cd3 = new BattleCard(7, CardType.SPELL, "joro", 10, 5);

        //Act
        ra.Add(cd1);
        ra.Add(cd2);
        ra.Add(cd3);

        //Assert
        Assert.That(ra.GetById(5), Is.EqualTo(cd1));
        Assert.That(
            ra.GetById(7)
            , Is.Not.EqualTo(new BattleCard(53, CardType.RANGED, "a", 10, 5)));
    }

    [Test]
    public void ChangeCardType_ShouldWorkCorrectly_On_Existingcd()
    {
        //Arrange
        IArena ra = new RoyaleArena.RoyaleArena();
        var cd1 = new BattleCard(5, CardType.SPELL, "joro", 10, 5);
        var cd2 = new BattleCard(6, CardType.SPELL, "joro", 10, 5);
        var cd3 = new BattleCard(7, CardType.SPELL, "joro", 10, 5);

        //Act
        ra.Add(cd1);
        ra.Add(cd2);
        ra.Add(cd3);
        ra.ChangeCardType(5, CardType.MELEE);

        //Assert
        Assert.That(cd1.Type, Is.EqualTo(CardType.MELEE));
        Assert.That(ra.Count, Is.EqualTo(3));
    }

    [Test]
    public void RA_ShouldBeEnumeratedIn_InsertionOrder()
    {
        //Arrange
        IArena ra = new RoyaleArena.RoyaleArena();
        var cd1 = new BattleCard(5, CardType.SPELL, "joro", 5, 5);
        var cd2 = new BattleCard(6, CardType.SPELL, "joro", 6, 5);
        var cd3 = new BattleCard(7, CardType.SPELL, "joro", 7, 5);
        var expected = new List<BattleCard>() { cd1, cd3, cd2 };

        //Act
        ra.Add(cd1);
        ra.Add(cd3);
        ra.Add(cd2);

        var actual = new List<BattleCard>();

        foreach (var battlecard in ra) actual.Add(battlecard);

        //Assert
        CollectionAssert.AreEqual(expected, actual);
    }

    [Test]
    public void RA_ShouldReturn_BattlecardsInCorrectOrder_AfterDelete()
    {
        //Arrange
        IArena ra = new RoyaleArena.RoyaleArena();
        var cd1 = new BattleCard(5, CardType.SPELL, "joro", 10, 5);
        var cd2 = new BattleCard(6, CardType.SPELL, "joro", 11, 5);
        var cd3 = new BattleCard(7, CardType.SPELL, "joro", 12, 5);
        var expected = new List<BattleCard>() { cd2 };

        //Act
        ra.Add(cd1);
        ra.Add(cd3);
        ra.Add(cd2);
        ra.RemoveById(5);
        ra.RemoveById(7);
        var actual = ra.ToList();

        //Assert
        CollectionAssert.AreEqual(expected, actual);
    }

    [Test]
    public void GetInSwagRange_ShouldReturn_CorrectBattlecards()
    {
        //Arrange
        IArena ra = new RoyaleArena.RoyaleArena();
        var cd1 = new BattleCard(5, CardType.SPELL, "dragon", 8, 1);
        var cd2 = new BattleCard(6, CardType.SPELL, "raa", 7, 2);
        var cd3 = new BattleCard(7, CardType.SPELL, "maga", 6, 5.5);
        var cd4 = new BattleCard(12, CardType.SPELL, "shuba", 5, 15.6);
        var cd5 = new BattleCard(15, CardType.SPELL, "tanuki", 5, 7.8);
        var expected = new List<BattleCard>() { cd5, cd4 };

        //Act
        ra.Add(cd1);
        ra.Add(cd3);
        ra.Add(cd2);
        ra.Add(cd4);
        ra.Add(cd5);
        var battleCards = ra.GetAllInSwagRange(7, 16);
        var actual = battleCards.ToList();

        //Assert
        CollectionAssert.AreEqual(expected, actual);
    }

    [Test]
    public void FindFirstLeastSwag_ShouldWorkCorrectly()
    {
        //Arrange
        IArena ra = new RoyaleArena.RoyaleArena();
        var cd1 = new BattleCard(5, CardType.SPELL, "joro", 6.0, 1.0);
        var cd2 = new BattleCard(6, CardType.MELEE, "joro", 7.0, 5.5);
        var cd3 = new BattleCard(7, CardType.SPELL, "joro", 11.0, 5.5);
        var cd4 = new BattleCard(12, CardType.BUILDING, "joro", 12.0, 15.6);
        var cd5 = new BattleCard(15, CardType.BUILDING, "moro", 13.0, 7.8);
        var expected = new List<BattleCard>() { cd1, cd2, cd3, cd5 };

        //Act
        ra.Add(cd1);
        ra.Add(cd3);
        ra.Add(cd2);
        ra.Add(cd4);
        ra.Add(cd5);
        var battleCards = ra.FindFirstLeastSwag(4);

        var actual = battleCards.ToList();

        //Assert
        CollectionAssert.AreEqual(expected, actual);
    }

    [Test]
    public void GetByTypeAndDamageRangeOrderedByDamageThenById_ShouldWorkCorrectly_On_CorrectRange()
    {
        //Arrange
        IArena ra = new RoyaleArena.RoyaleArena();
        var cd1 = new BattleCard(5, CardType.SPELL, "joro", 1, 5);
        var cd2 = new BattleCard(6, CardType.SPELL, "joro", 5.5, 6);
        var cd3 = new BattleCard(7, CardType.SPELL, "joro", 5.5, 7);
        var cd4 = new BattleCard(12, CardType.SPELL, "joro", 15.6, 10);
        var cd5 = new BattleCard(15, CardType.RANGED, "joro", 7.8, 6);
        var expected = new List<BattleCard>() { cd4, cd2, cd3, cd1 };

        //Act
        ra.Add(cd1);
        ra.Add(cd3);
        ra.Add(cd2);
        ra.Add(cd4);
        ra.Add(cd5);
        //Assert
        var battleCards = ra.GetByTypeAndDamageRangeOrderedByDamageThenById(CardType.SPELL, 0, 20);
        var actual = battleCards.ToList();
        CollectionAssert.AreEqual(expected, actual);
    }

    [Test]
    public void GetByName_ShouldWorkCorrectly()
    {
        //Arrange
        IArena ra = new RoyaleArena.RoyaleArena();
        var cd1 = new BattleCard(2, CardType.SPELL, "joro", 5, 1);
        var cd2 = new BattleCard(1, CardType.SPELL, "joro", 6, 1);
        var cd3 = new BattleCard(4, CardType.SPELL, "joro", 7, 15.6);
        var cd4 = new BattleCard(3, CardType.SPELL, "joro", 8, 15.6);
        var cd5 = new BattleCard(8, CardType.RANGED, "joro", 11, 17.8);
        var expected = new List<BattleCard>() { cd5, cd4, cd3, cd2, cd1 };

        //Act
        ra.Add(cd1);
        ra.Add(cd3);
        ra.Add(cd2);
        ra.Add(cd4);
        ra.Add(cd5);

        //Assert
        var joro = ra.GetByNameOrderedBySwagDescending("joro");
        var actual = joro.ToList();

        CollectionAssert.AreEquivalent(expected, actual);
    }


    [Test]
    public void GetByCardTypeAndMaximumDamage_ShouldWorkCorrectly_OnExistingSender()
    {
        //Arrange
        IArena ra = new RoyaleArena.RoyaleArena();
        var cd1 = new BattleCard(2, CardType.SPELL, "joro", 1, 5);
        var cd2 = new BattleCard(1, CardType.SPELL, "valq", 14.8, 6);
        var cd3 = new BattleCard(3, CardType.SPELL, "valq", 15.6, 12);
        var cd4 = new BattleCard(4, CardType.SPELL, "valq", 15.6, 61);
        var cd5 = new BattleCard(8, CardType.SPELL, "valq", 17.8, 13);
        var expected = new List<BattleCard>() { cd3, cd4, cd2, cd1 };

        //Act
        ra.Add(cd1);
        ra.Add(cd3);
        ra.Add(cd2);
        ra.Add(cd4);
        ra.Add(cd5);

        //Assert
        var battlecards = ra.GetByCardTypeAndMaximumDamage(CardType.SPELL, 15.6);
        var actual = battlecards.ToList();
        CollectionAssert.AreEqual(expected, actual);
    }

    [Test]
    public void GetByCardType_ShouldReturnCorrectResultAfterRemove()
    {
        //Arrange
        IArena ra = new RoyaleArena.RoyaleArena();
        var cd1 = new BattleCard(2, CardType.SPELL, "valq", 2, 14.8);
        var cd2 = new BattleCard(1, CardType.SPELL, "valq", 2, 14.8);
        var cd3 = new BattleCard(4, CardType.SPELL, "valq", 4, 15.6);
        var cd4 = new BattleCard(3, CardType.SPELL, "valq", 3, 15.6);
        var cd5 = new BattleCard(8, CardType.RANGED, "valq", 8, 17.8);
        var expected = new List<BattleCard>() { cd3, cd2, cd1 };

        //Act
        ra.Add(cd1);
        ra.Add(cd3);
        ra.Add(cd2);
        ra.Add(cd4);
        ra.Add(cd5);
        ra.RemoveById(8);
        ra.RemoveById(3);

        //Assert
        var battlecards = ra.GetByCardType(CardType.SPELL);
        var actual = battlecards.ToList();
        CollectionAssert.AreEquivalent(expected, actual);
    }
}