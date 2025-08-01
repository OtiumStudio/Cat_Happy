using Cysharp.Threading.Tasks;
using HC.Data;
using HC.Event;
using HC.Resource;
using HC.Utils;
using NUnit.Framework.Constraints;
using UGS;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

namespace HC.Game
{
    public class Cat_Actor : MonoBehaviour
    {
        int catCode;
        SpriteRenderer spriteRenderer;
        Animator animator;
        NavMeshAgent agent;
        E_ANIMATION catState = E_ANIMATION.IDLE;
        static float agentDrift = 0.0001f;

        E_DESTINATION destination = E_DESTINATION.NONE;
        public GuestTable.Data CatData { get; set; }
        public FoodData foodData;


        private void Awake()
        {
            gameObject.SetActive(false);
            agent = GetComponent<NavMeshAgent>();
            agent.updateRotation = false;
            agent.updateUpAxis = false;

            spriteRenderer = GetComponent<SpriteRenderer>();
            animator = GetComponent<Animator>();
        }
        private void Update()
        {
            if(catState == E_ANIMATION.WALK && Vector3.Distance(agent.destination, transform.position) < 1f)
            {
                if(destination == E_DESTINATION.TABLE)
                {
                    GameEvent.ServiceEvents.Emit(new DestinationEvent(this, E_DESTINATION.TABLE, foodData.TableIndex));
                }
                ChangeCatState(E_ANIMATION.IDLE);
            }
        }
        private void OnDestroy()
        {
            UnBind();
        }
        public async void Init(int catCode)
        {
            this.catCode = catCode;

            Bind();
            CatData = UGSManager.GetData<GuestTable.Data>(catCode);
            agent.enabled = false;
            transform.position = CatPathManager.GetStartPosition();
            agent.enabled = true;
            gameObject.SetActive(true);
            animator.runtimeAnimatorController = await LoadAddressableManager.Load_AnimController<RuntimeAnimatorController>(CatData.icon);
            GameEvent.ServiceEvents.Emit(new JoinCatEvent(this));

        }

        public void Close()
        {
            Destroy(gameObject);
        }

        void Bind()
        {
            GameEvent.ServiceEvents.On<FinishCookingEvent>(OnEating);
        }
        void UnBind()
        {
            GameEvent.ServiceEvents.Off<FinishCookingEvent>(OnEating);
        }

        public void SetDestination(Vector3 target)
        {
            destination = E_DESTINATION.NONE;
            var driftPos = target;
            if (Mathf.Abs(transform.position.x - target.x) < agentDrift)
                driftPos = target + new Vector3(agentDrift, 0f, 0f);
            agent.SetDestination(driftPos);
            ChangeCatState(E_ANIMATION.WALK);
        }
        public void SetTableDestination(FoodData foodData)
        {
            this.foodData = foodData;
            SetDestination(CatPathManager.GetTablePosition(foodData.TableIndex));
            destination = E_DESTINATION.TABLE;
        }

        public void ChangeCatState(E_ANIMATION state)
        {
            animator.SetInteger("a_State", (int)state);
            catState = state;
            switch (state)
            {
                case E_ANIMATION.IDLE:
                    break;
                case E_ANIMATION.WALK:
                    break;
                case E_ANIMATION.EAT: 
                    break;
                case E_ANIMATION.SAD: 
                    break;
                case E_ANIMATION.SIT: 
                    break;
            }
        }

        async void OnEating(FinishCookingEvent e)
        {
            if (foodData == null || foodData.TableIndex != e.FoodData.TableIndex) return;

            ChangeCatState(E_ANIMATION.EAT);
            await UniTask.Delay(5000);
            GameEvent.ServiceEvents.Emit(new FinishEatingEvent(foodData.TableIndex));
            foodData = null;
        }
    }
}
