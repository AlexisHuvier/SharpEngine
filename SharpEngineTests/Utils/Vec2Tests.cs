using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpEngine;

namespace SharpEngineTests.Utils
{
    [TestClass]
    public class Vec2Tests
    {

        [TestMethod]
        public void Constructors()
        {
            Vec2 vec = new Vec2(2);
            Vec2 vec2 = new Vec2(2, 2);

            Assert.AreEqual(2, vec.x);
            Assert.AreEqual(2, vec.y);
            Assert.AreEqual(2, vec2.x);
            Assert.AreEqual(2, vec2.y);
            Assert.AreEqual(vec2, vec);
        }

        [TestMethod]
        public void Length_LengthSquared()
        {
            Vec2 vec = new Vec2(3, 4);

            Assert.AreEqual(5, vec.Length());
            Assert.AreEqual(25, vec.LengthSquared());
        }

        [TestMethod]
        public void Normalized()
        {
            Vec2 vec = new Vec2(5, 0);
            Vec2 vec2 = new Vec2(0, 5);
            Vec2 vec3 = new Vec2(3, 4);

            Assert.AreEqual(new Vec2(1, 0), vec.Normalized());
            Assert.AreEqual(new Vec2(0, 1), vec2.Normalized());
            Assert.AreEqual(new Vec2(.6f, .8f), vec3.Normalized());
        }

        [TestMethod]
        public void Convert()
        {
            Assert.IsInstanceOfType(new Vec2(0).ToMG(), typeof(Microsoft.Xna.Framework.Vector2));
            Assert.IsInstanceOfType(new Vec2(0).ToAetherPhysics(), typeof(tainicom.Aether.Physics2D.Common.Vector2));
        }

        [TestMethod]
        public void toString() => Assert.AreEqual($"Vec2(x=1, y=1)", new Vec2(1).ToString());

        [TestMethod]
        public void ArithmeticOperations()
        {
            Vec2 vec = new Vec2(2, 3);
            Assert.AreEqual(new Vec2(2, 3), vec);
            vec += new Vec2(2, 1);
            Assert.AreEqual(new Vec2(4, 4), vec);
            vec += 2f;
            Assert.AreEqual(new Vec2(6, 6), vec);
            vec /= new Vec2(2, 3);
            Assert.AreEqual(new Vec2(3, 2), vec);
            vec /= 2f;
            Assert.AreEqual(new Vec2(1.5f, 1), vec);
            vec *= new Vec2(4, 3);
            Assert.AreEqual(new Vec2(6, 3), vec);
            vec *= 3f;
            Assert.AreEqual(new Vec2(18, 9), vec);
            vec -= new Vec2(8, 5);
            Assert.AreEqual(new Vec2(10, 4), vec);
            vec -= 1f;
            Assert.AreEqual(new Vec2(9, 3), vec);
            vec = -vec;
            Assert.AreEqual(new Vec2(-9, -3), vec);
        }
    }
}
