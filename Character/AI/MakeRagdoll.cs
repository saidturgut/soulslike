using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeRagdoll : MonoBehaviour
{
    private Animator animator;

    private GameObject[] bones;

    private Transform ragdollComps;

    public Vector3 killerDirection;

    private void Start()
    {
        killerDirection = -transform.forward;

        ragdollComps = Resources.Load<Transform>("AI/RagdollComponents");

        animator = GetComponent<Animator>();

        bones = new GameObject[11];

        bones[0] = animator.GetBoneTransform(HumanBodyBones.Hips).gameObject;
        bones[1] = animator.GetBoneTransform(HumanBodyBones.LeftUpperLeg).gameObject;
        bones[2] = animator.GetBoneTransform(HumanBodyBones.LeftLowerLeg).gameObject;
        bones[3] = animator.GetBoneTransform(HumanBodyBones.RightUpperLeg).gameObject;
        bones[4] = animator.GetBoneTransform(HumanBodyBones.RightLowerLeg).gameObject;
        bones[5] = animator.GetBoneTransform(HumanBodyBones.Chest).gameObject;
        bones[6] = animator.GetBoneTransform(HumanBodyBones.LeftUpperArm).gameObject;
        bones[7] = animator.GetBoneTransform(HumanBodyBones.LeftLowerArm).gameObject;
        bones[8] = animator.GetBoneTransform(HumanBodyBones.Head).gameObject;
        bones[9] = animator.GetBoneTransform(HumanBodyBones.RightUpperArm).gameObject;
        bones[10] = animator.GetBoneTransform(HumanBodyBones.RightLowerArm).gameObject;

        foreach(GameObject g in bones)
        {
            Rigidbody r = g.AddComponent<Rigidbody>();

            r.mass = 70f;
        }

        BoxCollider b = bones[0].AddComponent<BoxCollider>();
        b.center = ragdollComps.transform.GetChild(0).GetComponent<BoxCollider>().center;
        b.size = ragdollComps.transform.GetChild(0).GetComponent<BoxCollider>().size;

        CapsuleCollider c1 = bones[1].AddComponent<CapsuleCollider>();
        CharacterJoint j1 = bones[1].AddComponent<CharacterJoint>();
        j1.connectedBody = bones[0].GetComponent<Rigidbody>();

        CapsuleCollider c2 = bones[2].AddComponent<CapsuleCollider>();
        CharacterJoint j2 = bones[2].AddComponent<CharacterJoint>();
        j2.connectedBody = bones[1].GetComponent<Rigidbody>();

        CapsuleCollider c3 = bones[3].AddComponent<CapsuleCollider>();
        CharacterJoint j3 = bones[3].AddComponent<CharacterJoint>();
        j3.connectedBody = bones[0].GetComponent<Rigidbody>();

        CapsuleCollider c4 = bones[4].AddComponent<CapsuleCollider>();
        CharacterJoint j4 = bones[4].AddComponent<CharacterJoint>();
        j4.connectedBody = bones[3].GetComponent<Rigidbody>();

        BoxCollider c5 = bones[5].AddComponent<BoxCollider>();
        c5.center = ragdollComps.transform.GetChild(5).GetComponent<BoxCollider>().center;
        c5.size = ragdollComps.transform.GetChild(5).GetComponent<BoxCollider>().size;
        CharacterJoint j5 = bones[5].AddComponent<CharacterJoint>();
        j5.connectedBody = bones[0].GetComponent<Rigidbody>();

        CapsuleCollider c6 = bones[6].AddComponent<CapsuleCollider>();
        CharacterJoint j6 = bones[6].AddComponent<CharacterJoint>();
        j6.connectedBody = bones[5].GetComponent<Rigidbody>();

        CapsuleCollider c7 = bones[7].AddComponent<CapsuleCollider>();
        CharacterJoint j7 = bones[7].AddComponent<CharacterJoint>();
        j7.connectedBody = bones[6].GetComponent<Rigidbody>();

        SphereCollider c8 = bones[8].AddComponent<SphereCollider>();
        c8.center = ragdollComps.transform.GetChild(8).GetComponent<SphereCollider>().center;
        c8.radius = ragdollComps.transform.GetChild(8).GetComponent<SphereCollider>().radius;
        CharacterJoint j8 = bones[8].AddComponent<CharacterJoint>();
        j8.connectedBody = bones[5].GetComponent<Rigidbody>();

        CapsuleCollider c9 = bones[9].AddComponent<CapsuleCollider>();
        CharacterJoint j9 = bones[9].AddComponent<CharacterJoint>();
        j9.connectedBody = bones[5].GetComponent<Rigidbody>();

        CapsuleCollider c10 = bones[10].AddComponent<CapsuleCollider>();
        CharacterJoint j10 = bones[10].AddComponent<CharacterJoint>();
        j10.connectedBody = bones[9].GetComponent<Rigidbody>();

        for (int i = 1; i < bones.Length; i++)
        {
            if (i != 0 && i != 5 && i != 8)
            {
                CapsuleCollider g = bones[i].AddComponent<CapsuleCollider>();
                g.center = ragdollComps.transform.GetChild(i).GetComponent<CapsuleCollider>().center;
                g.radius = ragdollComps.transform.GetChild(i).GetComponent<CapsuleCollider>().radius;
                g.height = ragdollComps.transform.GetChild(i).GetComponent<CapsuleCollider>().height;
            }

            bones[i].GetComponent<CharacterJoint>().axis = ragdollComps.transform.GetChild(i).GetComponent<CharacterJoint>().axis;
            bones[i].GetComponent<CharacterJoint>().swingAxis = ragdollComps.transform.GetChild(i).GetComponent<CharacterJoint>().swingAxis;
            bones[i].GetComponent<CharacterJoint>().highTwistLimit = ragdollComps.transform.GetChild(i).GetComponent<CharacterJoint>().highTwistLimit;
            bones[i].GetComponent<CharacterJoint>().lowTwistLimit = ragdollComps.transform.GetChild(i).GetComponent<CharacterJoint>().lowTwistLimit;
            bones[i].GetComponent<CharacterJoint>().swing1Limit = ragdollComps.transform.GetChild(i).GetComponent<CharacterJoint>().swing1Limit;
            bones[i].GetComponent<CharacterJoint>().swing2Limit = ragdollComps.transform.GetChild(i).GetComponent<CharacterJoint>().swing2Limit;

            bones[i].layer = 6;
        }

        foreach (GameObject g in bones)
        {
            if (g.GetComponent<CharacterJoint>())
            {
                g.GetComponent<CharacterJoint>().enablePreprocessing = false;
            }
        }

        bones[Random.Range(5,11)].GetComponent<Rigidbody>().AddForce(killerDirection * 150000);

        /*BoxCollider bbb = gameObject.AddComponent<BoxCollider>();
        bbb.isTrigger = true;
        bbb.center = ragdollComps.GetComponent<BoxCollider>().center;
        bbb.size = ragdollComps.GetComponent<BoxCollider>().size;*/
    }
}
