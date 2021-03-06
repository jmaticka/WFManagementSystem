﻿using System.Collections.Generic;
using WFMDatabase.Entities;

namespace WFMDatabase.DML
{
    public interface IDMLBlockType
    {
        List<BlockType> GetAll();
        BlockType GetById(int Id);
        BlockType Insert(BlockType blockType);
        BlockType Update(BlockType blockType);
        BlockType Delete(BlockType blockType);
    }
}
