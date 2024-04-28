using System.Collections.Generic;

public class XDRVChartBeat
{
    public XDRVChartRow[] Rows;

    public XDRVChartBeat()
    {
        // We'll have a max of 192nd quantization, so 48 rows per beat
        Rows = new XDRVChartRow[XDRVChartUtils.ROWS_PER_BEAT];

        for (int i = 0; i < XDRVChartUtils.ROWS_PER_BEAT; i++)
        {
            Rows[i] = new XDRVChartRow();
        }
    }

    public void SetData(List<XDRVChartRow> data)
    {
        int rowMult = (int)(XDRVChartUtils.ROWS_PER_BEAT / (float)data.Count);

        for (int i = 0; i < data.Count; i++)
        {
            Rows[rowMult * i] = data[i];
        }
    }

    private string Shrink()
    {
        // find highest quant (192nd, 64th, 48th, etc)

        int rowsFilled = 0;

        foreach (XDRVChartRow row in Rows)
        {
            if (row.IsEmpty())
                continue;

            rowsFilled++;
        }

        string beatString = "";

        int rowSpacing = 48;

        int[] quantRowSpacing = { 48, 24, 16, 12, 8, 6, 4, 3, 1 };

        for (int q = 0; q < quantRowSpacing.Length; q++)
        {
            int nonEmptyRowCount = 0;
            int searchRowSpacing = quantRowSpacing[q];

            for (int i = 0; i < Rows.Length; i += searchRowSpacing)
            {
                if (!Rows[i].IsEmpty())
                    nonEmptyRowCount++;
            }

            if (nonEmptyRowCount >= rowsFilled)
            {
                rowSpacing = quantRowSpacing[q];
                break;
            }
        }

        for (int i = 0; i < Rows.Length; i += rowSpacing)
        {
            beatString += Rows[i].ToString();
            beatString += '\n';
        }

        return beatString;
    }

    public new string ToString()
    {
        return Shrink();
    }
}
