using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Eleonora.Core.Models;

namespace Eleonora.Droid.Adapters
{
    class SearchAdapter : BaseAdapter<Search>
    {
        private List<Search> listSearch;
        private Context context;

        public SearchAdapter(Context context, IEnumerable<Search> listSearch)
        {
            this.context = context;
            this.listSearch = listSearch.ToList();
        }


        public override Java.Lang.Object GetItem(int position)
        {
            return position;
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var view = convertView;
            SearchAdapterViewHolder holder = null;

            if (view != null)
                holder = view.Tag as SearchAdapterViewHolder;

            if (holder == null)
            {
                holder = new SearchAdapterViewHolder();
                view = LayoutInflater.From(context).Inflate(Resource.Layout.item_card, null, false);

                holder.Title = view.FindViewById<TextView>(Resource.Id.Title);
                holder.Description = view.FindViewById<TextView>(Resource.Id.Description);
                // Mock
                holder.Title.Text = "Texto Demo";
                holder.Description.Text = @"Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed pellentesque auctor dui nec ultricies. Cras sed massa et nunc mattis congue ac eget mauris. Suspendisse egestas massa in lorem eleifend, vitae gravida felis imperdiet. Curabitur a libero id lacus posuere imperdiet. In hac habitasse platea dictumst. Vivamus gravida aliquam blandit. Proin et orci nec orci pharetra cursus id ac ante. Aliquam non elit a lectus faucibus viverra rhoncus vehicula tortor. Sed nec egestas purus. Sed eget sagittis dui.";
                view.Tag = holder;
                var inflater = context.GetSystemService(Context.LayoutInflaterService).JavaCast<LayoutInflater>();
                //replace with your item and your holder items
                //comment back in
                //view = inflater.Inflate(Resource.Layout.item_, parent, false);
                //holder.Title = view.FindViewById<TextView>(Resource.Id.text);
            }


            //fill in your items
            //holder.Title.Text = "new text here";

            return view;
        }

        //Fill in cound here, currently 0
        public override int Count => listSearch.Count;

        public override Search this[int position]
        {
            get { return listSearch[position]; }
        }

        public void Add(Search item)
        {
            listSearch.Add(item);
            NotifyDataSetChanged();
        }

        public void Clear()
        {
            listSearch.Clear();
            NotifyDataSetChanged();
        }
    }

    class SearchAdapterViewHolder : Java.Lang.Object
    {
        //Your adapter views to re-use
        public TextView Title { get; set; }
        public TextView Description { get; set; }
    }
}