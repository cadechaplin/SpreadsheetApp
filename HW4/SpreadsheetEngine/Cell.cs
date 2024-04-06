// <copyright file="Cell.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using System.Xml;

namespace SpreadsheetEngine;

#pragma warning disable SA1309
using System.ComponentModel;

/// <summary>
/// Abstract Cell class.
/// </summary>
public abstract class Cell : INotifyPropertyChanged
{
#pragma warning disable SA1306
#pragma warning disable SA1401
    /// <summary>
    /// Stores the data of Text.
    /// </summary>
    protected string StoredText;

    /// <summary>
    /// Stores the data of Value.
    /// </summary>
    protected string StoredValue;
#pragma warning restore SA1401
#pragma warning restore SA1306
    private readonly int _rowIndex;
    private readonly int _columnIndex;
    private uint _backgroundColor;

    /// <summary>
    /// Initializes a new instance of the <see cref="Cell"/> class.
    /// </summary>
    /// <param name="setRowIndex">Integer that sets the row Index.</param>
    /// <param name="setColumnIndex">Integer that sets the column Index.</param>
    public Cell(int setRowIndex, int setColumnIndex)
    {
        this._rowIndex = setRowIndex;
        this._columnIndex = setColumnIndex;
        this.StoredText = string.Empty;
        this.StoredValue = string.Empty;
    }

    /// <summary>
    /// Event to fire when changing a property.
    /// </summary>
    public event PropertyChangedEventHandler? PropertyChanged = (sender, e) => { };

    /// <summary>
    /// Gets or Sets property StoredText.
    /// </summary>
    public abstract string Value { get; set; }

    /// <summary>
    /// Gets or sets StoreText.
    /// </summary>
    public string Text
    {
        get => this.StoredText;

        set
        {
            if (this.StoredText == value)
            {
                return;
            }

            this.StoredText = value;
            this.OnPropertyChanged(nameof(this.Text));
        }
    }

    /// <summary>
    /// Gets or sets Background color.
    /// </summary>
    public uint BackgroundColor
    {
        get => this._backgroundColor;

        set
        {
            if (value == this._backgroundColor)
            {
                return;
            }

            this._backgroundColor = value;
            this.OnPropertyChanged(nameof(this.BackgroundColor));
        }
    }

    /// <summary>
    /// Gets _rowIndex.
    /// </summary>
    public int RowIndex => this._rowIndex;

    /// <summary>
    /// Gets _columnIndex.
    /// </summary>
    public int ColumnIndex => this._columnIndex;

    public XmlNode ToXMLNode(XmlDocument xmlDoc)
    {
        XmlNode cellNode = xmlDoc.CreateElement("Cell");
        XmlNode rowNode = xmlDoc.CreateElement("Row");
        rowNode.InnerText = this.RowIndex.ToString();
        cellNode.AppendChild(rowNode);

        XmlNode columnNode = xmlDoc.CreateElement("Column");
        columnNode.InnerText = this.ColumnIndex.ToString();
        cellNode.AppendChild(columnNode);

        XmlNode textNode = xmlDoc.CreateElement("Text");
        textNode.InnerText = Text;
        cellNode.AppendChild(textNode);

        XmlNode backgroundColorNode = xmlDoc.CreateElement("BackgroundColor");
        backgroundColorNode.InnerText = this.BackgroundColor.ToString();
        cellNode.AppendChild(backgroundColorNode);

        return cellNode;
    }

    /// <summary>
    /// Function to call when changing a property.
    /// </summary>
    /// <param name="propertyName">Name of changing property.</param>
    protected virtual void OnPropertyChanged(string propertyName)
    {
        this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}