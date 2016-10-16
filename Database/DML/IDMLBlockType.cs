using WFMDatabase.Entities;

namespace WFMDatabase.DML
{
    interface IDMLBlockType
    {
        BlockType Insert(BlockType blockType);
        BlockType Update(BlockType blockType);
        BlockType Delete(BlockType blockType);
    }
}
