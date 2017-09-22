using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using frame8.Logic.Misc.Visual.UI.ScrollRectItemsAdapter;
using frame8.Logic.Misc.Other.Extensions;


namespace frame8.ScrollRectItemsAdapter.SimpleLoopingSpinnerExample
{
    /// <summary>Very basic example with a spinner that loops its items, similarly to a time picker in an alarm app</summary>
    public class SimpleLoopingSpinnerExample : MonoBehaviour
    {
        /// <summary>Configuration visible in the inspector</summary>
        public MyParams adapterParams;

        // Instance of your ScrollRectItemsAdapter8 implementation
        MyScrollRectAdapter _Adapter;


        IEnumerator Start()
        {
             _Adapter = new MyScrollRectAdapter();

            // Wait 3 frames
            yield return null;
            yield return null;
            yield return null;

            // Initialize the adapter after 3 frames
            // You can also try calling Canvas.ForceUpdateCanvases() instead if you for some reason can't wait 3 frames, although it wasn't tested
            _Adapter.Init(adapterParams);
            _Adapter.ChangeItemCountTo(adapterParams.NumberOfItems);
		}

        void OnDestroy()
        {
            // The adapter has some resources that need to be disposed after you destroy the scroll view
            if (_Adapter != null)
                _Adapter.Dispose();
        }

        void Update()
        {
            // Parameters are null until Init() is called, so this is an indicator that the adapter was not yet initialized. See DelayedInit() above
            if (_Adapter.Parameters == null)
                return;

            adapterParams.currentSelectedIndicatorText.text = "Selected: ";
            if (_Adapter.VisibleItemsCount == 0)
                return;

            int middleVHIndex = _Adapter.VisibleItemsCount / 2;
            var middleVH = _Adapter.GetItemViewsHolder(middleVHIndex);

            adapterParams.currentSelectedIndicatorText.text += "#" + middleVH.itemIndex + ", value=" + adapterParams.GetItemValueAtIndex(middleVH.itemIndex);
            middleVH.background.color = adapterParams.selectedColor;


            for (int i = 0; i < _Adapter.VisibleItemsCount; ++i)
            {
                if (i != middleVHIndex)
                    _Adapter.GetItemViewsHolder(i).background.color = adapterParams.nonSelectedColor;
            }
        }

		public void SmoothScrollToCurrent()
		{
			adapterParams.NextScrollToItemIndicatorButton.interactable = false;
			_Adapter.SmoothScrollTo(adapterParams.NextScrollToItemIndex, 1f, .5f, .5f, progress =>
			{
				if (progress == 1f) // done
					adapterParams.SetRandomNextScrollToItem(); // change it & update button interactability
				return true;
			});
		}

        /// <summary>Adds 10 items</summary>
        public void Add10More()
        {
            _Adapter.ChangeItemCountTo(adapterParams.NumberOfItems + 10);
        }

        /// <summary>Removes 20 items</summary>
        public void Remove20()
        {
            _Adapter.ChangeItemCountTo(Math.Max(0, adapterParams.NumberOfItems - 20));
        }

        #region ScrollRectItemsAdapter8 code
        [Serializable] // serializable, so it can be shown in inspector
        public class MyParams : BaseParams
        {
            public RectTransform prefab;
            public int startItemNumber = 0;
            public int increment = 1;
            public Color selectedColor, nonSelectedColor;
            public Text currentSelectedIndicatorText;
            public Text nextScrollToItemIndicatorText;

			public int NumberOfItems { get { return _NumberOfItems; } set { _NumberOfItems = value; SetRandomNextScrollToItem(); } }
			public int NextScrollToItemIndex { get; private set; }
			public Button NextScrollToItemIndicatorButton { get { return nextScrollToItemIndicatorText.transform.parent.GetComponent<Button>(); } }

			[SerializeField]
            int _NumberOfItems = 10;

            
			/// <summary>The value of each item is calculated dynamically using its <paramref name="index"/>, <see cref="startItemNumber"/> and the <see cref="increment"/><summary>
			/// <returns>The item's value (the displayed number)</returns>
			public int GetItemValueAtIndex(int index) { return startItemNumber + increment * index; }

			public void SetRandomNextScrollToItem()
			{
				if (_NumberOfItems > 0)
					NextScrollToItemIndex = UnityEngine.Random.Range(0, _NumberOfItems);
				else
				{
					NextScrollToItemIndex = -1;
				}
				NextScrollToItemIndicatorButton.interactable = _NumberOfItems > 0;
				nextScrollToItemIndicatorText.text = "Go to value: " + GetItemValueAtIndex(NextScrollToItemIndex);
			}
        }

        // Self-explanatory
        public class MyItemViewsHolder : BaseItemViewsHolder
        {
            public Image background;
            public Text titleText;

            public override void CollectViews()
            {
                base.CollectViews();

                background = root.GetComponent<Image>();
                titleText = root.GetComponentInChildren<Text>();
            }
        }

        /// <summary>Minimal implementation of the adapter that initializes & updates the viewholders. The size of each item is fixed in this case and it's the same as the prefab's</summary>
        public class MyScrollRectAdapter : ScrollRectItemsAdapter8<MyParams, MyItemViewsHolder>
		{
			public override void ChangeItemCountTo(int itemsCount)
			{
				_Params.NumberOfItems = itemsCount;

				base.ChangeItemCountTo(itemsCount);
			}

			// Will be called for vertical scroll views
			protected override float GetItemHeight(int index) { return _Params.prefab.rect.height; }

            // Will be called for horizontal scroll views
            protected override float GetItemWidth(int index) { return _Params.prefab.rect.width; }

            protected override MyItemViewsHolder CreateViewsHolder(int itemIndex)
            {
                var instance = new MyItemViewsHolder();
                instance.Init(_Params.prefab, itemIndex);

                return instance;
            }

            // Here's the meat of the whole recycling process
            protected override void UpdateViewsHolder(MyItemViewsHolder newOrRecycled)
            {
                newOrRecycled.titleText.text = _Params.GetItemValueAtIndex(newOrRecycled.itemIndex) + "";
                newOrRecycled.background.color = Color.white;
            }
		}
        #endregion

    }
}
