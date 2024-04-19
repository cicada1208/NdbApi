using Lib;
using Models;
using Params;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Repositorys
{
    public class SchemaRepository : BaseRepository<Schema>
    {
        public ApiResult<string> CreateModel(Schema schema)
        {
            DBUtil dbUtil = null;
            string dbTypeVar = string.Empty;
            string dbNameVar = string.Empty;
            DataTable dtSchema = null;
            DataTable dtTables = null;
            var modelBuilder = new StringBuilder();
            string tab = new String(' ', 4);

            if (schema.DBName == DBParam.DBName.NIS)
            {
                dbUtil = DB.NIS;
                dbTypeVar = nameof(DBParam.DBType.SYBASE);
                dbNameVar = nameof(DBParam.DBName.NIS);
            }
            else if (schema.DBName == DBParam.DBName.SYB1)
            {
                dbUtil = DB.Syb1;
                dbTypeVar = nameof(DBParam.DBType.SYBASE);
                dbNameVar = nameof(DBParam.DBName.SYB1);
            }
            else if (schema.DBName == DBParam.DBName.SYB2)
            {
                dbUtil = DB.Syb2;
                dbTypeVar = nameof(DBParam.DBType.SYBASE);
                dbNameVar = nameof(DBParam.DBName.SYB2);
            }

            dtSchema = dbUtil.Query(schema.Sql);

            if (dtSchema.Rows.Count != 0)
            {
                string tableName = string.Empty;
                string className = string.Empty;
                List<string> pkList = null;
                dtTables = dtSchema.DefaultView.ToTable(true, new string[] { "BaseTableName" });
                if (dtTables.Rows.Count == 1)
                {
                    tableName = dtTables.Rows[0]["BaseTableName"].ToString();
                    // primary key 目前從這裡取得較為正確
                    ////var index_keys = dbUtil.Query<dynamic>($"sp_helpindex {tableName}").ToList().FirstOrDefault()?.index_keys;
                    //var index_keys = dbUtil.Query<Schema>($"sp_helpindex {tableName}").ToList()
                    //    .OrderBy(key => key.index_name).FirstOrDefault()?.index_keys;
                    var index_keys = dbUtil.Query<Schema>($"sp_helpindex {tableName}").ToList();
                    if (index_keys.Exists(key => key.index_name.Contains("PK")))
                        index_keys = index_keys.Where(key => key.index_name.Contains("PK")).ToList();
                    if (index_keys.Count != 1)
                    {
                        index_keys = index_keys.Where(key =>
                        {
                            bool rst = false;
                            foreach (var desp in key.index_description.Split(','))
                                if (desp.Trim() == "unique")
                                {
                                    rst = true;
                                    break;
                                }
                            return rst;
                        }).ToList();
                        if (index_keys.Count != 1)
                            index_keys = index_keys.Where(key =>
                            {
                                bool rst = false;
                                foreach (var desp in key.index_description.Split(','))
                                    if (desp.Trim() == "clustered")
                                    {
                                        rst = true;
                                        break;
                                    }
                                return rst;
                            }).ToList();
                    }
                    var pk = index_keys.OrderBy(key => key.index_name).FirstOrDefault()?.index_keys;
                    pkList = (pk as string)?.Split(',').Select(key => key = key.Trim()).ToList();
                }
                foreach (DataRow row in dtTables.Rows)
                    className += (className != string.Empty ? "_" : string.Empty) + Regex.Replace(row["BaseTableName"].ToString(), "^ni_", string.Empty).ToUpperFirstChar();
                modelBuilder.AppendLine("using Params;");
                modelBuilder.AppendLine("using System;");
                modelBuilder.AppendLine("using System.ComponentModel.DataAnnotations;");
                modelBuilder.AppendLine("using System.ComponentModel.DataAnnotations.Schema;");
                modelBuilder.AppendLine();
                modelBuilder.AppendLine("namespace Models");
                modelBuilder.AppendLine("{");

                modelBuilder.AppendLine($"{tab}[Lib.Attributes.Table(DBParam.DBType.{dbTypeVar}, DBParam.DBName.{dbNameVar}" +
                $"{(tableName != string.Empty ? $", \"{tableName}\"" : string.Empty)})]");
                modelBuilder.AppendFormat($"{tab}public class {className} : BaseModel<{className}>{Environment.NewLine}");
                modelBuilder.AppendLine($"{tab}{{");

                foreach (DataRow row in dtSchema.Rows)
                {
                    var type = (Type)row["DataType"];
                    var typeName = DBParam.ColumnTypeAliases.ContainsKey(type) ? DBParam.ColumnTypeAliases[type] : type.Name;
                    // NullableTypes: 這些型別強制Nullable，避免new Model時會有預設值的狀況
                    //var isNullable = (bool)row["AllowDBNull"] && NullableTypes.Contains(type);
                    var isNullable = DBParam.ColumnNullableTypes.Contains(type);
                    var collumnName = (string)row["ColumnName"];
                    var isKey = pkList?.Contains(collumnName) ?? false; //(bool)row["IsKey"];

                    //modelBuilder.AppendLine($"{tab + tab}public {typeName}{(isNullable ? "?" : string.Empty)} {collumnName} {{ get; set; }}");
                    modelBuilder.AppendLine($"{tab + tab}private {typeName}{(isNullable ? "?" : string.Empty)} _{collumnName};");
                    if (isKey) modelBuilder.AppendLine($"{tab + tab}[Key]");
                    modelBuilder.AppendLine($"{tab + tab}public {typeName}{(isNullable ? "?" : string.Empty)} {collumnName}");
                    modelBuilder.AppendLine($"{tab + tab}{{");
                    modelBuilder.AppendLine($"{tab + tab + tab}get => _{collumnName};");
                    modelBuilder.AppendLine($"{tab + tab + tab}set => Set(ref _{collumnName}, value);");
                    modelBuilder.AppendLine($"{tab + tab}}}");
                    modelBuilder.AppendLine();
                }

                modelBuilder.AppendLine($"{tab}}}");
                modelBuilder.AppendLine("}");
                //builder.AppendLine();
            }

            //do
            //{
            //    if (reader.FieldCount <= 1) continue;

            //    var schema = reader.GetSchemaTable();
            //    foreach (DataRow row in schema.Rows)
            //    {
            //        if (string.IsNullOrWhiteSpace(builder.ToString()))
            //        {
            //            var tableName = row["BaseTableName"];
            //            builder.AppendFormat("public class {0}{1}", tableName, Environment.NewLine);
            //            builder.AppendLine("{");
            //        }

            //        var type = (Type)row["DataType"];
            //        var name = TypeAliases.ContainsKey(type) ? TypeAliases[type] : type.Name;
            //        var isNullable = (bool)row["AllowDBNull"] && NullableTypes.Contains(type);
            //        var collumnName = (string)row["ColumnName"];

            //        builder.AppendLine(string.Format("\tpublic {0}{1} {2} {{ get; set; }}", name, isNullable ? "?" : string.Empty, collumnName));
            //        builder.AppendLine();
            //    }

            //    builder.AppendLine("}");
            //    builder.AppendLine();
            //} while (reader.NextResult());

            dtSchema?.Dispose();
            dtTables?.Dispose();

            var result = new ApiResult<string>(modelBuilder.ToString());
            return result;
        }

    }
}
