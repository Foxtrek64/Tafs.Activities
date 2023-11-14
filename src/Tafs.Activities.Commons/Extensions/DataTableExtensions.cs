//
//  DataTableExtensions.cs
//
//  Author:
//       Devin Duanne <dduanne@tafs.com>
//
//  Copyright (c) TAFS, LLC.
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU Lesser General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU Lesser General Public License for more details.
//
//  You should have received a copy of the GNU Lesser General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.
//

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tafs.Activities.Commons.Extensions
{
    /// <summary>
    /// A collection of extensions for <see cref="DataTable"/>.
    /// </summary>
    public static class DataTableExtensions
    {
        /// <summary>
        /// Renders the DataTable as a text-based ascii table for use in message boxes, logging, etc..
        /// </summary>
        /// <param name="table">The datatable to process.</param>
        /// <param name="rowNumbers">If <see langword="true"/>, will add an extra column on the left with the current row number.</param>
        /// <param name="border">If <see langword="true"/>, will draw a border with ASCII characters; otherwise, will only draw a separator between header and rows.</param>
        public static void AsAsciiTable(this DataTable table, bool rowNumbers = false, bool border = true)
        {
            static string GetSeparatorRow(int[] lengths, char left, char middle, char right, char horizontal)
            {
                StringBuilder rowBuilder = new();
                for (var j = 0; j < lengths.Length; j++)
                {
                    if (j == 0)
                    {
                        rowBuilder.Append(left);
                        rowBuilder.Append(new string(horizontal, lengths[j] + 2));
                    }
                    else if (j < lengths.Length - 1)
                    {
                        rowBuilder.Append(middle);
                        rowBuilder.Append(new string(horizontal, lengths[j] + 2));
                    }
                    else
                    {
                        rowBuilder.Append(right);
                    }
                }

                return rowBuilder.ToString();
            }

            DataTable workingTable = table.Copy();

            if (rowNumbers)
            {
                workingTable = new DataTable();
                var indexColumn = workingTable.Columns.Add("#", typeof(int));
                indexColumn.SetOrdinal(0);
                int index = 0;
                table.Rows.Cast<DataRow>().ForEach(row => row[indexColumn] = ++index);
            }
            else
            {
                workingTable = table.Copy();
            }

            char cornerTopLeft, cornerTopMiddle, cornerTopRight;
            char cornerMiddleLeft, cornerMiddleMiddle, cornerMiddleRight;
            char cornerBottomLeft, cornerBottomMiddle, cornerBottomRight;
            char headerVertical, headerHorizontal;
            char separatorVertical, separatorHorizontal;

            // Calculate the max width of each column.
            int colCount = rowNumbers ? table.Columns.Count + 1 : table.Columns.Count;
            var colLengths = new int[colCount];
            for (var i = 0; i < colCount; i++)
            {
                colLengths[i] = table.Columns.Cast<DataRow>().Max(x => x[i].ToString()?.Length ?? 0);
            }

            bool hasHeaderSeparators = true;
            bool hasLineSeparators = false;
            bool hasTopLine = true;
            bool hasBottomLine = true;
            bool hasLeftSide = true;
            bool hasRightSide = true;
            bool topLineUsesBodySeparators = false;
            string align = "left-align";

            // Map of variable locations in the output:
            //
            // [cTL]   [hdH]  [cTM]   [hdH]  [cTR]
            // [hdV] Header 1 [hdV] Header 2 [hdV]
            // [cML]   [hdH]  [cMM]   [hdH]  [cMR]
            // [spV] Value 1  [spV] Value 2  [spV]
            // [cML]   [spH]  [cMM]   [spH]  [cMR]
            // [spV] Value 1a [spV] Value 2a [spV]
            // [cBL]   [spH]  [cBM]   [spH]  [cBR]

            if (border)
            {
                // ASCII (Compact)
                hasTopLine = false;
                hasBottomLine = false;
                cornerTopLeft = cornerTopMiddle = cornerTopRight = ' ';
                cornerMiddleLeft = cornerMiddleMiddle = cornerMiddleRight = ' ';
                cornerBottomLeft = cornerBottomMiddle = cornerBottomRight = ' ';
                headerVertical = ' ';
                headerHorizontal = '-';
                separatorVertical = ' ';
                separatorHorizontal = '-';
            }
            else
            {
                // Unicode (Single Line)
                cornerTopLeft = '\u250C';
                cornerTopMiddle = '\u252C';
                cornerTopRight = '\u2510';

                cornerMiddleLeft = '\u251C';
                cornerMiddleMiddle = '\u253C';
                cornerMiddleRight = '\u2524';

                cornerBottomLeft = '\u2514';
                cornerBottomMiddle = '\u2534';
                cornerBottomRight = '\u2518';

                headerVertical = separatorVertical = '\u2502';
                headerHorizontal = separatorHorizontal = '\u2500';
            }

            StringBuilder sb = new();

            if (hasTopLine)
            {
                char topLineHorizontal = topLineUsesBodySeparators ? separatorHorizontal : headerHorizontal;
                sb.AppendLine(GetSeparatorRow(colLengths, cornerTopLeft, cornerTopMiddle, cornerTopRight, topLineHorizontal));
            }

            for (int i = 0; i < table.Rows.Count; i++)
            {
                // Separator Rows
                if (hasHeaderSeparators && i == 1)
                {
                    sb.AppendLine(GetSeparatorRow(colLengths, cornerMiddleLeft, cornerMiddleMiddle, cornerMiddleRight, headerHorizontal));
                }
                else if (hasLineSeparators && i > 1 && i < table.Rows.Count)
                {
                    sb.AppendLine(GetSeparatorRow(colLengths, cornerMiddleLeft, cornerMiddleMiddle, cornerMiddleRight, separatorHorizontal));
                }

                for (int j = 0; j <= colLengths.Length; j++)
                {
                    string data = table.Rows[i][j].ToString() ?? string.Empty;

                    char verticalBar = i == 0 ? headerVertical : separatorVertical;
                    if (j < colLengths.Length)
                    {
                        // TODO: Complete me!
                    }
                }
            }
        }
    }
}
