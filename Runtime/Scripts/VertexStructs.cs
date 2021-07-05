// Copyright 2020-2021 Andreas Atteneder
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//      http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//

using System.Runtime.InteropServices;

namespace GLTFast.Vertex
{
#if BURST
    using Unity.Mathematics;
#else
    using UnityEngine;
#endif

    [StructLayout(LayoutKind.Sequential)]
    struct VPosNormTan {
#if BURST
        public float3 pos;
        public float3 nrm;
        public float3 tan;
#else
        public Vector3 pos;
        public Vector3 nrm;
        public Vector4 tan;
#endif
    }

    [StructLayout(LayoutKind.Sequential)]
    struct VPosNorm {
#if BURST
        public float3 pos;
        public float3 nrm;
#else
        public Vector3 pos;
        public Vector3 nrm;
#endif
    }

    [StructLayout(LayoutKind.Sequential)]
    struct VPos {
#if BURST
        public float3 pos;
#else
        public Vector3 pos;
#endif
    }

    [StructLayout(LayoutKind.Sequential)]
    struct VTexCoord1 {
        public Vector2 uv0;
    }

    [StructLayout(LayoutKind.Sequential)]
    struct VTexCoord2 {
        public Vector2 uv0;
        public Vector2 uv1;
    }


    [StructLayout(LayoutKind.Sequential)]
    unsafe struct VBones {
        public fixed float weights[4];
        public fixed uint joints[4];

        public bool isSorted =>
            weights[0] >= weights[1]
            && weights[1] >= weights[2]
            && weights[2] >= weights[3];

        public void Sort() {
            for (int i = 0; i < 3; i++) {
                for (int j = i+1; j < 4; j++) {
                    if (weights[i] < weights[j]) {
                        var tmpWeight = weights[j];
                        weights[j] = weights[i];
                        weights[i] = tmpWeight;

                        var tmpJoint = joints[j];
                        joints[j] = joints[i];
                        joints[i] = tmpJoint;
                    }
                }
            }
        }
    }
}
