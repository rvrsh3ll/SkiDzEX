 public static int[] rndsizevalues = new int[] { 1, 2, 4, 8, 12, 16 };
        public static Dictionary<int, Tuple<TypeDef, int>> Dick = new Dictionary<int, Tuple<TypeDef, int>>();
        static int abc = 0;
        public static void StructGenerator(MethodDef method, ref int i)
        {
            if (method.Body.Instructions[i].IsLdcI4())
            {
                ITypeDefOrRef valueTypeRef = new Importer(method.Module).Import(typeof(System.ValueType));
                TypeDef structDef = new TypeDefUser(RandomString(rnd.Next(10, 30), ""), valueTypeRef);
                Tuple<TypeDef, int> outTuple;
                structDef.ClassLayout = new ClassLayoutUser(1, 0);
                structDef.Attributes |= TypeAttributes.Sealed | TypeAttributes.SequentialLayout | TypeAttributes.Public;
                List<Type> retList = new List<Type>();
                int rand = rndsizevalues[rnd.Next(0, 6)];
                retList.Add(GetType(rand));
                retList.ForEach(x => structDef.Fields.Add(new FieldDefUser(RandomString(rnd.Next(10, 30), ""), new FieldSig(new Importer(method.Module).Import(x).ToTypeSig()), FieldAttributes.Public)));
                int operand = method.Body.Instructions[i].GetLdcI4Value();
                if (abc < 25)
                {
                    method.Module.Types.Add(structDef);
                    Dick.Add(abc++, new Tuple<TypeDef, int>(structDef, rand));
                    int conta = operand - rand;
                    method.Body.Instructions[i].Operand = conta;
                    method.Body.Instructions[i].OpCode = OpCodes.Ldc_I4;
                    method.Body.Instructions.Insert(i + 1, Instruction.Create(OpCodes.Sizeof, structDef));
                    method.Body.Instructions.Insert(i + 2, Instruction.Create(OpCodes.Add));
                }
                else
                {
                    Dick.TryGetValue(rnd.Next(1, 24), out outTuple);
                    int conta = operand - outTuple.Item2;
                    method.Body.Instructions[i].Operand = conta;
                    method.Body.Instructions[i].OpCode = OpCodes.Ldc_I4;
                    method.Body.Instructions.Insert(i + 1, Instruction.Create(OpCodes.Sizeof, outTuple.Item1));
                    method.Body.Instructions.Insert(i + 2, Instruction.Create(OpCodes.Add));
                }
            }
        }