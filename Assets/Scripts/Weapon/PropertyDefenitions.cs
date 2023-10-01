using UnityEngine;

public interface IProperty
{
    void Execute(Rigidbody2D rb);
}

public class SetGravity : IProperty
{
    PropertyValue property;
    public SetGravity(PropertyValue property)
    {
        this.property = property;
    }

    public void Execute(Rigidbody2D rb)
    {
        var value = ((GravityValue)property).GetValue();

        rb.gravityScale = value ? 5 : 0;

    }
}

public class AddFroce : IProperty
{
    PropertyValue property;
    public AddFroce(PropertyValue property)
    {
        this.property = property;
    }

    public void Execute(Rigidbody2D rb)
    {
        var value = ((ForceDirectionValue)property).GetValue();

        var dir = Quaternion.AngleAxis(value, Vector3.forward) * Vector3.up;

        rb.AddForceAtPosition(dir * 800, rb.position);

    }
}

public class SetSize : IProperty
{
    PropertyValue property;
    public SetSize(PropertyValue property)
    {
        this.property = property;
    }

    public void Execute(Rigidbody2D rb)
    {
        var value = ((SizeValue)property).GetValue();

        rb.transform.localScale *= value ? 2 : 0.5f;

    }
}

