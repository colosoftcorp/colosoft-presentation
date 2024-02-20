using System.Collections.Generic;
using System.ComponentModel;

namespace Colosoft.Presentation.PresentationData
{
    /// <summary>
    /// Armazena os dados do item do QAT ou (Quick Access Toolbar).
    /// </summary>
    public class QatItem
    {
        private List<int> controlIndices;

        public int TabIndex { get; set; }

        public int GroupIndex { get; set; }

#pragma warning disable CA2227 // Collection properties should be read only
        public List<int> ControlIndices
#pragma warning restore CA2227 // Collection properties should be read only
        {
            get
            {
                if (this.controlIndices == null)
                {
                    this.controlIndices = new List<int>();
                }

                return this.controlIndices;
            }
            set
            {
                this.controlIndices = value;
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public object Instance { get; set; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsSplitHeader { get; set; }

        public QatItem()
        {
        }

        public QatItem(object instance, bool isSplitHeader)
        {
            this.Instance = instance;
            this.IsSplitHeader = isSplitHeader;
        }
    }
}
