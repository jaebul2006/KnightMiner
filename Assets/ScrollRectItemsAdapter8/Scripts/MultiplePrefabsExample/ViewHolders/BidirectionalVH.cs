﻿using frame8.Logic.Misc.Visual.UI.ScrollRectItemsAdapter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine.UI;
using frame8.ScrollRectItemsAdapter.MultiplePrefabsExample.Models;

namespace frame8.ScrollRectItemsAdapter.MultiplePrefabsExample.ViewHolders
{
    /// <summary>The view holder that can preset a <see cref="BidirectionalModel"/>. It demonstrates the flow of data both from the view to the model and vice-versa</summary>
    public class BidirectionalVH : BaseVH
    {
        public SliderItemBehaviour sliderBehaviour { get; private set; }

        BidirectionalModel _Model;


        public override void CollectViews()
        {
            base.CollectViews();

            sliderBehaviour = root.GetComponent<SliderItemBehaviour>();

            sliderBehaviour.ValueChanged += SliderBehaviour_ValueChanged;
        }

        /// <summary>Can only preset models of type <see cref="BidirectionalModel"/></summary>
        public override bool CanPresentModelType(Type modelType) { return modelType == typeof(BidirectionalModel); }

        internal override void UpdateViews(BaseModel model)
        {
            base.UpdateViews(model);

            _Model = null; // preventing setting _Model.value in the next SliderBehaviour_ValueChanged event, since we already know it
            var modelAsBidirectional = model as BidirectionalModel;
            sliderBehaviour.Value = modelAsBidirectional.value; // this will fire the SliderBehaviour_ValueChanged event below
            _Model = modelAsBidirectional;
        }

        void SliderBehaviour_ValueChanged(float newValue)
        {
            if (_Model != null)
                _Model.value = newValue;
        }
    }
}
