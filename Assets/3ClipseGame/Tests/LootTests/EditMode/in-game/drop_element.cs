using _3ClipseGame.Steam.Mechanics.LootSystem.InGame.Scripts.Dropper;
using NUnit.Framework;
using UnityEngine;

public class drop_element
{
    private DropElement _dropElement;

    [Test]
    public void test_get_final_amount_many_times_always_same()
    {
        var isSuccessful = true;
        var lootAmount = _dropElement.GetFinalAmountOfDrop();

        for (var i = 0; i < 10; i++)
            if (_dropElement.GetFinalAmountOfDrop() != lootAmount)
                isSuccessful = false;

        Assert.IsTrue(isSuccessful);
    }

    [SetUp]
    public void Init()
    {
        var randomAmount = Random.Range(5, 20);
        var randomChance = Random.Range(0.2f, 0.8f);
        _dropElement = new DropElement(randomAmount, randomChance);
    }
}