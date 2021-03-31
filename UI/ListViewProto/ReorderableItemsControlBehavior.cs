using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

// http://yujiro15.net/blog/index.php?id=151
// http://yujiro15.net/YKSoftware/tips_DragAndDropList.html
// https://www.codeproject.com/Articles/1236549/Csharp-WPF-listview-Drag-Drop-a-Custom-Item
// https://www.codeproject.com/Articles/17266/Drag-and-Drop-Items-in-a-WPF-ListView
// https://github.com/amazingant/ListViewDragDropManager
namespace MyLog.UI.ListViewProto {
    class ReorderableItemsControlBehavior {
        #region Callback 添付プロパティ

        /// <summary>
        /// Callback 添付プロパティの定義
        /// </summary>
        public static readonly DependencyProperty CallbackProperty = DependencyProperty.RegisterAttached("Callback", typeof(Action<int>), typeof(ReorderableItemsControlBehavior), new PropertyMetadata(null, OnCallbackPropertyChanged));

        /// <summary>
        /// Callback 添付プロパティを取得します。
        /// </summary>
        /// <param name="target">対象とする DependencyObject を指定します。</param>
        /// <returns>取得した値を返します。</returns>
        public static Action<int> GetCallback(DependencyObject target) {
            return (Action<int>)target.GetValue(CallbackProperty);
        }

        /// <summary>
        /// Callback 添付プロパティを設定します。
        /// </summary>
        /// <param name="target">対象とする DependencyObject を指定します。</param>
        /// <param name="value">設定する値を指定します。</param>
        public static void SetCallback(DependencyObject target, Action<int> value) {
            target.SetValue(CallbackProperty, value);
        }

        /// <summary>
        /// Callback 添付プロパティ変更イベントハンドラ
        /// </summary>
        /// <param name="d">イベント発行元</param>
        /// <param name="e">イベント引数</param>
        private static void OnCallbackPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var itemsControl = d as ItemsControl;
            if (itemsControl == null) return;

            if (GetCallback(itemsControl) != null) {
                itemsControl.PreviewMouseLeftButtonDown += OnPreviewMouseLeftButtonDown;
                itemsControl.PreviewMouseMove += OnPreviewMouseMove;
                itemsControl.PreviewMouseLeftButtonUp += OnPreviewMouseLeftButtonUp;
                itemsControl.PreviewDragEnter += OnPreviewDragEnter;
                itemsControl.PreviewDragLeave += OnPreviewDragLeave;
                itemsControl.PreviewDrop += OnPreviewDrop;
            } else {
                itemsControl.PreviewMouseLeftButtonDown -= OnPreviewMouseLeftButtonDown;
                itemsControl.PreviewMouseMove -= OnPreviewMouseMove;
                itemsControl.PreviewMouseLeftButtonUp -= OnPreviewMouseLeftButtonUp;
                itemsControl.PreviewDragEnter -= OnPreviewDragEnter;
                itemsControl.PreviewDragLeave -= OnPreviewDragLeave;
                itemsControl.PreviewDrop -= OnPreviewDrop;
            }
        }

        #endregion Callback 添付プロパティ

        #region イベントハンドラ

        /// <summary>
        /// PreviewMouseLeftButtonDown イベントハンドラ
        /// </summary>
        /// <param name="sender">イベント発行元</param>
        /// <param name="e">イベント引数</param>
        private static void OnPreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e) {
            throw new NotImplementedException();
        }

        /// <summary>
        /// PreviewMouseMove イベントハンドラ
        /// </summary>
        /// <param name="sender">イベント発行元</param>
        /// <param name="e">イベント引数</param>
        private static void OnPreviewMouseMove(object sender, MouseEventArgs e) {
            throw new NotImplementedException();
        }

        /// <summary>
        /// PreviewMouseLeftButtonUp イベントハンドラ
        /// </summary>
        /// <param name="sender">イベント発行元</param>
        /// <param name="e">イベント引数</param>
        private static void OnPreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e) {
            throw new NotImplementedException();
        }

        /// <summary>
        /// PreviewDragEnter イベントハンドラ
        /// </summary>
        /// <param name="sender">イベント発行元</param>
        /// <param name="e">イベント引数</param>
        private static void OnPreviewDragEnter(object sender, DragEventArgs e) {
            throw new NotImplementedException();
        }

        /// <summary>
        /// PreviewDragLeave イベントハンドラ
        /// </summary>
        /// <param name="sender">イベント発行元</param>
        /// <param name="e">イベント引数</param>
        private static void OnPreviewDragLeave(object sender, DragEventArgs e) {
            throw new NotImplementedException();
        }

        /// <summary>
        /// PreviewDrop イベントハンドラ
        /// </summary>
        /// <param name="sender">イベント発行元</param>
        /// <param name="e">イベント引数</param>
        private static void OnPreviewDrop(object sender, DragEventArgs e) {
            throw new NotImplementedException();
        }

        #endregion イベントハンドラ
    }
}
