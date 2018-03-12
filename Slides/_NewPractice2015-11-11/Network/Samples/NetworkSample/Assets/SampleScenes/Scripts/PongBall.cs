using UnityEngine;
using UnityEngine.Networking;
using System.Collections;


namespace UnityStandardAssets.Network
{
    //Handle the pong ball. It is networked, and all operation are donne by the server
    //The synchronization on the clients are done via the NetworkTransform
    [RequireComponent(typeof(NetworkTransform))]
    public class PongBall : NetworkBehaviour
    {
        public bool isFrozen = false;

        protected Rigidbody _rigidbody;

        void Awake()
        {
            Collider c = GetComponent<Collider>();
            _rigidbody = GetComponent<Rigidbody>();

            c.material.frictionCombine = PhysicMaterialCombine.Minimum;
            c.material.dynamicFriction = 0.0f;
            c.material.staticFriction = 0.0f;
            _rigidbody.useGravity = false;
        }

        void Start()
        {
            if(isServer)
                ResetBall(0);
        }

        [ServerCallback]
        void OnTriggerEnter(Collider collision)
        {

            var playerZone = collision.gameObject.GetComponent<PongScoreZone>();
            if (playerZone != null)
            {
                playerZone.linkedPlayer.score += 1;
                PongManager.instance.CheckScores(); ;
                ResetBall(1 - playerZone.linkedPlayer.number);
            }
        }


        [ServerCallback]
        void OnCollisionExit(Collision collision)
        {
            if (collision.collider.GetComponent<UnityStandardAssets.Network.SimpleController>() != null)
            {//collision with a panel

                _rigidbody.velocity += _rigidbody.velocity.normalized * 0.5f;//accelerate the ball to ramp up difficulty

                Vector3 distanceToCenter = transform.position - collision.transform.position;

                float normalizedDist = distanceToCenter.y / collision.collider.bounds.extents.y;

                //add a force up or down depending on where on the panel the ball hits
                _rigidbody.AddForce(Vector3.up * normalizedDist * 200.0f);
            }

            //check if we arent going to "vertically" to avoid very long up/down bounce

            float d = Vector3.Dot(_rigidbody.velocity.normalized, Vector3.up);

            if (Mathf.Abs(d) > 0.9f)
            {
                _rigidbody.velocity = (_rigidbody.velocity + Vector3.right * Mathf.Sign(Vector3.Dot(_rigidbody.velocity, Vector3.right)) * 3.0f).normalized * _rigidbody.velocity.magnitude;
            }
        }

        [ServerCallback]
        void FixedUpdate()
        {
            if (isFrozen)
                _rigidbody.AddForce(-_rigidbody.velocity, ForceMode.VelocityChange); ;
        }

        public void Fire(Vector3 dir)
        {
            _rigidbody.isKinematic = false;
            _rigidbody.velocity = Vector3.zero;
            _rigidbody.AddForce(new Vector3(dir.x, dir.y, 0) * 5.0f, ForceMode.VelocityChange);
        }

        [Server]
        public void MoveBall(Vector3 newPos)
        {
            _rigidbody.MovePosition(newPos);
            RpcMoveBall(newPos);
        }

        //We use a RPC to "bypass" the NetworkTransform interpolation.
        //Effectivly "teleporting" the ball to the new pos instead of interpolation to it
        [ClientRpc]
        public void RpcMoveBall(Vector3 newPos)
        {
            _rigidbody.velocity = Vector3.zero;
            _rigidbody.MovePosition(newPos);
        }

        public void ResetBall(int paddle)
        {
            PongManager.Players[paddle].attachedBall = this;
            MoveBall(PongManager.Players[paddle].transform.position + (PongManager.Players[paddle].number == 0 ? Vector3.right : Vector3.left) * 0.3f);
            _rigidbody.isKinematic = true;
        }
    }
}