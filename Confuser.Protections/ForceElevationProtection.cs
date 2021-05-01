using System;
using System.Collections.Generic;
using System.Linq;
using Confuser.Core;
using Confuser.Core.Helpers;
using Confuser.Core.Services;
using Confuser.Renamer;
using dnlib.DotNet;
using dnlib.DotNet.Emit;

// some reason if i disable this it won't work.

namespace Confuser.Protections {
    [BeforeProtection(new string[]
    {
        "Ki.ControlFlow",
        "Ki.Constants"
    })]
    internal class ForceElevationProtection : Protection {
        public override string Name {
            get {
                return "Force Admin Priviliges Protection";
            }
        }

        public override string Description {
            get {
                return "This protection will force your file to run under Administrative priviliges to ensure that the process isn't easily tampered with.";
            }
        }

        public override string Id {
            get {
                return "force elevation";
            }
        }

        public override string FullId {
            get {
                return "Ki.ForceElevation";
            }
        }

        public override ProtectionPreset Preset {
            get {
                return ProtectionPreset.Maximum;
            }
        }

        protected override void Initialize(ConfuserContext context) {
        }

        protected override void PopulatePipeline(ProtectionPipeline pipeline) {
            pipeline.InsertPreStage(PipelineStage.ProcessModule, new ForceElevationProtection.ForceElevationPhase(this));
        }

        public const string _Id = "force elevation";
        public const string _FullId = "Ki.ForceElevation";
        private class ForceElevationPhase : ProtectionPhase {
            public ForceElevationPhase(ForceElevationProtection parent) : base(parent) {
            }

            public override ProtectionTargets Targets {
                get {
                    return ProtectionTargets.Modules;
                }
            }

            public override string Name {
                get {
                    return "Force Admin Priviliges";
                }
            }

            protected override void Execute(ConfuserContext context, ProtectionParameters parameters) {
                TypeDef runtimeType = context.Registry.GetService<IRuntimeService>().GetRuntimeType("Confuser.Runtime.ForceElevation");
                IMarkerService service = context.Registry.GetService<IMarkerService>();
                INameService service2 = context.Registry.GetService<INameService>();
                foreach (ModuleDef moduleDef in parameters.Targets.OfType<ModuleDef>()) {
                    IEnumerable<IDnlibDef> enumerable = InjectHelper.Inject(runtimeType, moduleDef.GlobalType, moduleDef);
                    MethodDef methodDef = moduleDef.GlobalType.FindStaticConstructor();
                    MethodDef method2 = (MethodDef)enumerable.Single((IDnlibDef method) => method.Name == "Init");
                    methodDef.Body.Instructions.Insert(0, Instruction.Create(OpCodes.Call, method2));
                    foreach (IDnlibDef def in enumerable) {
                        service2.MarkHelper(def, service, (Protection)base.Parent);
                    }
                }
            }
        }
    }
}
