﻿//# FancyScrollView

//A scrollview component that can implement highly flexible animation.
//It also supports infinite scrolling.
//This library is free and opensource on GitHub.More info, supports and sourcecodes, see[https://github.com/setchi/FancyScrollView](https://github.com/setchi/FancyScrollView)

//## License
//MIT

using UnityEngine;
using UnityEngine.UI;

namespace FancyScrollView
{
    public class Example02ScrollViewCell : FancyScrollViewCell<Example02CellDto, Example02ScrollViewContext>
    {
        [SerializeField]
        Animator animator;
        [SerializeField]
        Text message;
        [SerializeField]
        Image image;
        [SerializeField]
        Button button;

        static readonly int scrollTriggerHash = Animator.StringToHash("scroll");

        void Start()
        {
            button.onClick.AddListener(OnPressedCell);
        }

        /// <summary>
        /// Updates the content.
        /// </summary>
        /// <param name="itemData">Item data.</param>
        public override void UpdateContent(Example02CellDto itemData)
        {
            message.text = itemData.Message;

            if (Context != null)
            {
                var isSelected = Context.SelectedIndex == DataIndex;
                image.color = isSelected
                    ? new Color32(0, 255, 255, 100)
                    : new Color32(255, 255, 255, 77);
            }
        }

        /// <summary>
        /// Updates the position.
        /// </summary>
        /// <param name="position">Position.</param>
        public override void UpdatePosition(float position)
        {
            currentPosition = position;
            animator.Play(scrollTriggerHash, -1, position);
            animator.speed = 0;
        }

        void OnPressedCell()
        {
            if (Context != null)
            {
                Context.OnPressedCell(this);
            }
        }

        // GameObject が非アクティブになると Animator がリセットされてしまうため
        // 現在位置を保持しておいて OnEnable のタイミングで現在位置を再設定します
        float currentPosition = 0;
        void OnEnable()
        {
            UpdatePosition(currentPosition);
        }
    }
}
