namespace MD.SerializableDictionary
{
    public class SDictionaryElementBase { }

    [System.Serializable]
    public class SDictionaryElement<Tkey, Tval> : SDictionaryElementBase
    {
        public Tkey Key;
        public Tval Val;

        /// <summary>
        /// Constructor for dictionary element<br></br><br></br>
        /// </summary>
        public SDictionaryElement() { }

        /// <summary>
        /// Constructor for dictionary element<br></br><br></br>
        /// </summary>
        /// <param name="tKey"> Dictionary Element Key</param>
        /// <param name="tval1">Dictionary Element Value</param>
        public SDictionaryElement(Tkey tkey, Tval tval1)
        {
            this.Key = tkey;
            this.Val = tval1;
        }
    }
}
