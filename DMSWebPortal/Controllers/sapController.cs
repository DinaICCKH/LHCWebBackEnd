using DMSWebPortal.DTOs;
using DMSWebPortal.Models;
using DMSWebPortal.Services;
using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml.ExtendedProperties;
using DocumentFormat.OpenXml.InkML;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
//using Microsoft.Build.Framework;
using Microsoft.EntityFrameworkCore;
using Microsoft.ReportingServices.ReportProcessing.ReportObjectModel;
using Microsoft.VisualBasic;
using System.Drawing;

namespace DMSWebPortal.Controllers
{
    [ApiController]
    [Route("api/sap")]   // <-- base route: api/sap
    [Authorize] // Secure all methods in this controller
    public class sapController : ControllerBase
    {
        private readonly AppDbContext db;
        private List<Set_Object> set = null;
        public sapController(AppDbContext context)
        {
            db = context;
        }

        [HttpPost("uploadImage")]
        public async Task<object> uploadImage()
        {
            try
            {
                set = new List<Set_Object>();
                var form = HttpContext.Request.Form;
                string type = form["type"].ToString();
                string code = form["code"].ToString();
                try
                {
                    var file = form.Files.FirstOrDefault();
                    if (file == null || file.Length == 0)
                    {
                        return new { Status = "Error", Message = "No file uploaded." };
                    }
                    // Define upload path
                    var projectRoot = Directory.GetCurrentDirectory();
                    var uploadPath = Path.Combine(projectRoot, "wwwroot", "Image", type);
                    if (!Directory.Exists(uploadPath))
                    {
                        Directory.CreateDirectory(uploadPath);
                    }
                    // Sanitize filename
                    string newFileName = $"{code}{Path.GetExtension(file.FileName)}";
                    var filePath = Path.Combine(uploadPath, newFileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                    var relativePath = Path.Combine("Image", type, newFileName).Replace("\\", "/");
                    if (type.ToLower() == "item")
                    {
                        Add_Set("", code, "uploadItem", "OK");
                        var item = await db.tblItems.FirstOrDefaultAsync(a => a.ItemCode == code);
                        if (item != null)
                        {
                            item.ImageUrlServer = relativePath;
                            item.ImageUrlLocal = relativePath;
                            item.UpdatedDate = DateTime.Now;
                            db.tblItems.Update(item);
                           await db.SaveChangesAsync();
                        }
                    }
                    if (type.ToLower() == "bp")
                    {
                        Add_Set("", code, "uploadBP", "OK");

                        var bp = await db.tblBPs.FirstOrDefaultAsync(a => a.CardCode == code);
                        if (bp != null)
                        {
                            bp.ImageUrlServer = relativePath;
                            bp.ImagePath = relativePath;
                            db.tblBPs.Update(bp);
                            await db.SaveChangesAsync();
                        }
                    }
                }
                catch(Exception ex)
                {
                    if (type.ToLower() == "item")
                    {
                        Add_Set(ex.Message, code, "uploadItem", "Failed");
                    }
                    if (type.ToLower() == "bp")
                    {
                        Add_Set(ex.Message, code, "uploadBP", "Failed");
                    }
                }
                return new { Status = "Success", Message = "Upload Image inserted/updated successfully.", Set_Obj = set };
            }
            catch(Exception ex)
            {
                return new { Status = "Error", Message = "No file uploaded." };
            }
        }
        private async Task Add_Set(string message,string objcode,string objtype,string status)
        {
            if (set == null)
            {
                set = new List<Set_Object>();
            }
            set.Add(new Set_Object()
            {
                Message = message,
                ObjCode = objcode,
                ObjType = objtype,
                Status = status,
            });
        }
        
        [HttpPost("add_item")]
        public async Task<IActionResult> add_item([FromBody] List<v_Item> list)
        {
            if (list == null || !list.Any())
            {
                return BadRequest(new ApiResponse
                {
                    Code = 400,
                    Status = "Error",
                    Message = "Invalid item data."
                });
            }
            //using var transaction = await db.Database.BeginTransactionAsync();
            try
            {
                set = new List<Set_Object>();
                foreach (var x in list)
                {
                    try
                    {
                        var itm = await db.tblItems.FirstOrDefaultAsync(a => a.ItemCode == x.ItemCode);
                        if (itm == null)
                        {
                            db.tblItems.Add(new tblItem
                            {
                                ItemCode = x.ItemCode,
                                ItemName = x.ItemName,
                                ItemGroupCode = (short)x.ItemGroupCode,
                                ItemGroupName = x.ItemGroupName,
                                UgpEntry = x.UgpEntry,
                                Onhand = x.Onhand,
                                OnOrder = x.OnOrder,
                                IsCommited = x.IsCommited,
                                Available = x.Available,
                                MinLevel = x.MinLevel,
                                MaxLevel = x.MaxLevel,
                                Status = x.Status,
                                FrgnName = x.FrgnName,
                                InvUoMCode = x.InvUoMcode,
                                InvUoMEntry = x.InvUoMentry,
                                U_ProGroup = x.UProGroup,
                                HasPromotion = x.HasPromotion,
                                UpdatedDate = DateTime.Now,
                                OcrCode = x.OcrCode,
                                OcrCode2 = x.OcrCode2,
                                OcrCode3 = x.OcrCode3,
                                OcrCode4 = x.OcrCode4,
                                PrincipleCode = x.PrincipleCode,
                                MainCat = x.MainCat,
                                SubCat = x.SubCat,
                                PackageType = x.PackageType,
                                BarCode = x.BarCode,
                                DefEntry = x.DefEntry,
                                AltQty = 0,
                            });
                        }
                        else
                        {
                            itm.ItemName = x.ItemName;
                            itm.ItemGroupCode = (short)x.ItemGroupCode;
                            itm.ItemGroupName = x.ItemGroupName;
                            itm.UgpEntry = x.UgpEntry;
                            itm.Onhand = x.Onhand;
                            itm.OnOrder = x.OnOrder;
                            itm.IsCommited = x.IsCommited;
                            itm.Available = x.Available;
                            itm.MinLevel = x.MinLevel;
                            itm.MaxLevel = x.MaxLevel;
                            itm.Status = x.Status;
                            itm.FrgnName = x.FrgnName;
                            itm.InvUoMCode = x.InvUoMcode;
                            itm.InvUoMEntry = x.InvUoMentry;
                            itm.U_ProGroup = x.UProGroup;
                            itm.HasPromotion = x.HasPromotion;
                            itm.UpdatedDate = DateTime.Now;
                            itm.OcrCode = x.OcrCode;
                            itm.OcrCode2 = x.OcrCode2;
                            itm.OcrCode3 = x.OcrCode3;
                            itm.OcrCode4 = x.OcrCode4;
                            itm.PrincipleCode = x.PrincipleCode;
                            itm.MainCat = x.MainCat;
                            itm.SubCat = x.SubCat;
                            itm.PackageType = x.PackageType;
                            itm.BarCode = x.BarCode;
                            itm.DefEntry = x.DefEntry;
                            itm.AltQty = 0;
                            db.tblItems.Update(itm);
                        }
                        await db.SaveChangesAsync();
                        //set.Add(new Set_Object()
                        //{
                        //    Message = "",
                        //    ObjCode = x.ItemCode,
                        //    ObjType = "add_item",
                        //    Status = "OK",
                        //});
                        await Add_Set("", x.ItemCode, "add_item", "OK");
                        //Pricing
                        try
                        {
                            if (x.Pricing != null && x.Pricing.Any())
                            {
                                foreach (var y in x.Pricing)
                                {
                                    var data = await db.tblItemPricings.FirstOrDefaultAsync(a => a.PricingKey == y.PricingKey);

                                    if (data == null)
                                    {
                                        db.tblItemPricings.Add(new tblItemPricing
                                        {
                                            PricingKey = y.PricingKey,
                                            ItemCode = y.ItemCode,
                                            PriceListCode = y.PriceListCode,
                                            UoMEntry = y.UoMentry,
                                            Amount = y.Amount,
                                        });
                                    }
                                    else
                                    {
                                        if (data.Amount != y.Amount)
                                        {
                                            data.Amount = y.Amount;
                                            db.tblItemPricings.Update(data);
                                        }
                                    }
                                    await db.SaveChangesAsync();
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                        }
                        //Stock
                        try
                        {
                            if (x.stocks != null && x.stocks.Any())
                            {
                                foreach (var z in x.stocks)
                                {
                                    var data = await db.tblItemStocks.FirstOrDefaultAsync(a => a.ItemStockKey == z.ItemStockKey);

                                    if (data == null)
                                    {
                                        db.tblItemStocks.Add(new tblItemStock
                                        {
                                            ItemStockKey = z.ItemStockKey,
                                            ItemCode = z.ItemCode,
                                            WhsCode = z.WhsCode,
                                            AltQty = z.AltQty,
                                            Available = z.Available,
                                            IsCommited = z.IsCommited,
                                            MaxStock = z.MaxStock,
                                            MinStock = z.MinStock,
                                            Onhand = z.Onhand,
                                            OnOrder = z.OnOrder,
                                            UpdateDate = DateTime.Now
                                        });
                                    }
                                    else
                                    {
                                        if (data.Onhand != z.Onhand || data.AltQty!=z.AltQty)
                                        {
                                            data.Onhand = z.Onhand;
                                            data.AltQty = z.AltQty;
                                            data.Available = z.Available;
                                            data.IsCommited = z.IsCommited;
                                            data.MaxStock = z.MaxStock;
                                            data.MinStock = z.MinStock;
                                            data.UpdateDate = DateTime.Now;
                                            db.tblItemStocks.Update(data);
                                        }
                                    }
                                    await db.SaveChangesAsync();
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                        }
                        //UoMGroup
                        try
                        {
                            if (x.UoMgroups != null && x.UoMgroups.Any())
                            {
                                foreach (var b in x.UoMgroups)
                                {
                                    var ugp = await db.tblUoMGroups.FirstOrDefaultAsync(a => a.UgpKey == b.UgpKey);

                                    if (ugp == null)
                                    {
                                        db.tblUoMGroups.Add(new tblUoMGroup
                                        {
                                            UgpKey = b.UgpKey,
                                            UgpEntry = b.UgpEntry,
                                            UgpName=b.UgpName,
                                            UoMEntry = b.UoMentry,
                                            UoMCode = b.UoMcode,
                                            AltQty = b.AltQty,
                                            BaseQty = b.BaseQty,

                                        });
                                    }
                                    else
                                    {
                                        if (ugp.BaseQty != b.BaseQty)
                                        {
                                            ugp.AltQty = b.AltQty;
                                            ugp.BaseQty = b.BaseQty;
                                            db.tblUoMGroups.Update(ugp);
                                        }
                                    }
                                    await db.SaveChangesAsync();
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                        }
                    }
                    catch(Exception ex)
                    {
                        //set.Add(new Set_Object()
                        //{
                        //    Message = ex.Message,
                        //    ObjCode = x.ItemCode,
                        //    ObjType = "add_item",
                        //    Status = "Failed",
                        //});
                        await Add_Set(ex.Message, x.ItemCode, "add_item", "Failed");
                    }
                }
                
                //await transaction.CommitAsync();

                return Ok(new ApiResponse
                {
                    Code = 200,
                    Status = "Success",
                    Message = "Items inserted/updated successfully.",
                    Set_Obj=set
                });
            }
            catch (Exception ex)
            {
                //await transaction.RollbackAsync();
                return StatusCode(500, new ApiResponse
                {
                    Code = 500,
                    Status = "Error",
                    Message =(ex.InnerException==null ? ex.Message : ex.InnerException.Message)
                });
            }
        }
        [HttpPost("add_principle")]
        public async Task<IActionResult> add_principle([FromBody] List<tblPrinciple> list)
        {
            if (list == null || !list.Any())
            {
                return BadRequest(new ApiResponse
                {
                    Code = 400,
                    Status = "Error",
                    Message = "Invalid Principle Data."
                });
            }
            try
            {
                set = new List<Set_Object>();
                foreach (var x in list)
                {
                    try
                    {
                        var prin = await db.tblPrinciples.FirstOrDefaultAsync(a => a.Code == x.Code);
                        if (prin == null)
                        {
                            db.tblPrinciples.Add(new tblPrinciple
                            {
                                Code = x.Code,
                                Dscription = x.Dscription,
                                DocStatus = x.DocStatus,
                                CreatedDate = DateTime.Now
                            });
                        }
                        else
                        {
                            prin.Dscription = x.Dscription;
                            prin.DocStatus = x.DocStatus;
                            prin.UpdateDate = DateTime.Now;
                            db.tblPrinciples.Update(prin);
                        }
                        await db.SaveChangesAsync();
                        Add_Set("", x.Code, "add_principle", "OK");
                    }
                    catch(Exception ex)
                    {
                        Add_Set(ex.Message, x.Code, "add_principle", "Failed");
                    }
                }
                return Ok(new ApiResponse
                {
                    Code = 200,
                    Status = "Success",
                    Message = "Principle inserted/updated successfully.",
                    Set_Obj=set
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse
                {
                    Code = 500,
                    Status = "Error",
                    Message = ex.Message
                });
            }
        }

        [HttpPost("add_maincat")]
        public async Task<IActionResult> add_maincat([FromBody] List<tblMainCat> list)
        {
            if (list == null || !list.Any())
            {
                return BadRequest(new ApiResponse
                {
                    Code = 400,
                    Status = "Error",
                    Message = "Invalid Main Category Data."
                });
            }
            try
            {
                set = new List<Set_Object>();
                foreach (var x in list)
                {
                    try
                    {
                        var main = await db.tblMainCats.FirstOrDefaultAsync(a => a.Code == x.Code);
                        if (main == null)
                        {
                            db.tblMainCats.Add(new tblMainCat
                            {
                                Code = x.Code,
                                Dscription = x.Dscription,
                                DocStatus = x.DocStatus,
                                CreatedDate = DateTime.Now

                            });
                        }
                        else
                        {
                            main.Dscription = x.Dscription;
                            main.DocStatus = x.DocStatus;
                            main.UpdateDate = DateTime.Now;
                            db.tblMainCats.Update(main);
                        }
                        await db.SaveChangesAsync();
                        Add_Set("", x.Code, "add_maincat", "OK");
                    }
                    catch(Exception ex)
                    {
                        Add_Set(ex.Message, x.Code, "add_maincat", "Failed");
                    }
                }
                return Ok(new ApiResponse
                {
                    Code = 200,
                    Status = "Success",
                    Message = "Main Category inserted/updated successfully.",
                    Set_Obj = set
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse
                {
                    Code = 500,
                    Status = "Error",
                    Message = ex.Message
                });
            }
        }
        [HttpPost("add_subcat")]
        public async Task<IActionResult> add_subcat([FromBody] List<tblSubCat> list)
        {
            if (list == null || !list.Any())
            {
                return BadRequest(new ApiResponse
                {
                    Code = 400,
                    Status = "Error",
                    Message = "Invalid Sub Category Data."
                });
            }
            try
            {
                set = new List<Set_Object>();
                foreach (var x in list)
                {
                    try
                    {
                        var subc = await db.tblSubCats.FirstOrDefaultAsync(a => a.Code == x.Code);
                        if (subc == null)
                        {
                            db.tblSubCats.Add(new tblSubCat
                            {
                                Code = x.Code,
                                Dscription = x.Dscription,
                                DocStatus = x.DocStatus,
                                CreatedDate = DateTime.Now

                            });
                        }
                        else
                        {
                            subc.Dscription = x.Dscription;
                            subc.DocStatus = x.DocStatus;
                            subc.UpdateDate = DateTime.Now;
                            db.tblSubCats.Update(subc);
                        }
                        await db.SaveChangesAsync();
                        Add_Set("", x.Code, "add_subcat", "OK");
                    }
                    catch(Exception ex)
                    {
                        Add_Set(ex.Message, x.Code, "add_subcat", "Failed");
                    }
                }
                return Ok(new ApiResponse
                {
                    Code = 200,
                    Status = "Success",
                    Message = "Sub Category inserted/updated successfully.",
                    Set_Obj = set
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse
                {
                    Code = 500,
                    Status = "Error",
                    Message = ex.Message
                });
            }
        }
        [HttpPost("add_uomgroup")]
        public async Task<IActionResult> add_uomgroup([FromBody] List<tblUoMGroup> list)
        {
            if (list == null || !list.Any())
            {
                return BadRequest(new ApiResponse
                {
                    Code = 400,
                    Status = "Error",
                    Message = "Invalid UoM Group Category Data."
                });
            }
            try
            {
                foreach (var x in list)
                {
                    var ugp = await db.tblUoMGroups.FirstOrDefaultAsync(a => a.UgpKey == x.UgpKey);
                    if (ugp == null)
                    {
                        db.tblUoMGroups.Add(new tblUoMGroup
                        {
                            UgpKey = x.UgpKey,
                            UgpEntry = x.UgpEntry,
                            UgpName=x.UgpName,
                            UoMEntry = x.UoMEntry,
                            UoMCode = x.UoMCode,
                            AltQty = x.AltQty,
                            BaseQty = x.BaseQty,
                        });
                    }
                    else
                    {
                        ugp.UgpEntry = x.UgpEntry;
                        ugp.UgpName = x.UgpName;
                        ugp.UoMEntry = x.UoMEntry;
                        ugp.UoMCode = x.UoMCode;
                        ugp.AltQty = x.AltQty;
                        ugp.BaseQty = x.BaseQty;
                        db.tblUoMGroups.Update(ugp);
                    }
                }
                await db.SaveChangesAsync();
                return Ok(new ApiResponse
                {
                    Code = 200,
                    Status = "Success",
                    Message = "UoM Group inserted/updated successfully."
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse
                {
                    Code = 500,
                    Status = "Error",
                    Message = ex.Message
                });
            }
        }

        [HttpPost("add_whs")]
        public async Task<IActionResult> add_whs([FromBody] List<tblWh> list)
        {
            if (list == null || !list.Any())
            {
                return BadRequest(new ApiResponse
                {
                    Code = 400,
                    Status = "Error",
                    Message = "Invalid Warehouse Data."
                });
            }
            try
            {
                set = new List<Set_Object>();
                foreach (var x in list)
                {
                    try
                    {
                        var whs = await db.tblWhs.FirstOrDefaultAsync(a => a.WhsCode == x.WhsCode);
                        if (whs == null)
                        {
                            db.tblWhs.Add(new tblWh
                            {
                                WhsCode = x.WhsCode,
                                WhsName = x.WhsName,
                                WhsStatus = x.WhsStatus

                            });
                        }
                        else
                        {
                            if (whs.WhsName != x.WhsName)
                            {
                                whs.WhsName = x.WhsName;
                            }
                            db.tblWhs.Update(whs);
                        }
                        await db.SaveChangesAsync();
                        Add_Set("", x.WhsCode, "add_whs", "OK");
                    }
                    catch(Exception ex)
                    {
                        Add_Set(ex.Message, x.WhsCode, "add_whs", "Failed");
                    }
                }
                return Ok(new ApiResponse
                {
                    Code = 200,
                    Status = "Success",
                    Message = "Warehouse inserted/updated successfully.",
                    Set_Obj = set
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse
                {
                    Code = 500,
                    Status = "Error",
                    Message = ex.Message
                });
            }
        }
        [HttpPost("add_itemprice")]
        public async Task<IActionResult> add_itemprice([FromBody] List<tblItemPricing> list)
        {
            if (list == null || !list.Any())
            {
                return BadRequest(new ApiResponse
                {
                    Code = 400,
                    Status = "Error",
                    Message = "Invalid Item Pricing Data."
                });
            }
            try
            {
                foreach (var x in list)
                {
                    var data = await db.tblItemPricings.FirstOrDefaultAsync(a => a.PricingKey == x.PricingKey);

                    if (data == null)
                    {
                        db.tblItemPricings.Add(new tblItemPricing
                        {
                            PricingKey = x.PricingKey,
                            ItemCode = x.ItemCode,
                            PriceListCode = x.PriceListCode,
                            UoMEntry = x.UoMEntry,
                            Amount = x.Amount,

                        });
                    }
                    else
                    {
                        if (data.Amount != x.Amount)
                        {
                            data.Amount = x.Amount;
                            db.tblItemPricings.Update(data);
                        }
                    }
                }
                await db.SaveChangesAsync();

                return Ok(new ApiResponse
                {
                    Code = 200,
                    Status = "Success",
                    Message = "Item Pricing inserted/updated successfully."
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse
                {
                    Code = 500,
                    Status = "Error",
                    Message = ex.Message
                });
            }
        }
        [HttpPost("add_itemstock")]
        public async Task<IActionResult> add_itemstock([FromBody] List<tblItemStock> list)
        {
            if (list == null || !list.Any())
            {
                return BadRequest(new ApiResponse
                {
                    Code = 400,
                    Status = "Error",
                    Message = "Invalid Item Stock Data."
                });
            }
            try
            {
                foreach (var x in list)
                {
                    var data = await db.tblItemStocks.FirstOrDefaultAsync(a => a.ItemStockKey == x.ItemStockKey);

                    if (data == null)
                    {
                        db.tblItemStocks.Add(new tblItemStock
                        {
                            ItemStockKey = x.ItemStockKey,
                            ItemCode = x.ItemCode,
                            WhsCode = x.WhsCode,
                            AltQty = x.AltQty,
                            Available = x.Available,
                            IsCommited = x.IsCommited,
                            MaxStock = x.MaxStock,
                            MinStock = x.MinStock,
                            Onhand = x.Onhand,
                            OnOrder = x.OnOrder,
                            UpdateDate = DateTime.Now
                        });
                    }
                    else
                    {
                        if (data.Onhand != x.Onhand)
                        {
                            data.Onhand = x.Onhand;
                            data.UpdateDate = DateTime.Now;
                        }
                        db.tblItemStocks.Update(data);
                    }
                }
                await db.SaveChangesAsync();
                return Ok(new ApiResponse
                {
                    Code = 200,
                    Status = "Success",
                    Message = "Item Stock inserted/updated successfully."
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse
                {
                    Code = 500,
                    Status = "Error",
                    Message = ex.Message
                });
            }
        }

        [HttpPost("add_regional")]
        public async Task<IActionResult> add_regional([FromBody] List<tblRegional> list)
        {
            if (list == null || !list.Any())
            {
                return BadRequest(new ApiResponse
                {
                    Code = 400,
                    Status = "Error",
                    Message = "Invalid Regional Data."
                });
            }
            try
            {
                foreach (var x in list)
                {
                    var data = await db.tblRegionals.FirstOrDefaultAsync(a => a.RegionalCode == x.RegionalCode);

                    if (data == null)
                    {
                        db.tblRegionals.Add(new tblRegional
                        {
                            RegionalCode = x.RegionalCode,
                            RegionalName = x.RegionalName,
                            Status = x.Status
                        });
                    }
                    else
                    {
                        if (data.RegionalName != x.RegionalName)
                        {
                            data.RegionalName = x.RegionalName;
                        }
                        data.CC3Loc = x.CC3Loc;
                        data.Status = x.Status;
                        db.tblRegionals.Update(data);
                    }
                    Add_Set("", x.RegionalCode.ToString(), "Add_Regional", "OK");
                }
                await db.SaveChangesAsync();
                return Ok(new ApiResponse
                {
                    Code = 200,
                    Status = "Success",
                    Message = "Regional Data inserted/updated successfully.",
                    Set_Obj = set
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse
                {
                    Code = 500,
                    Status = "Error",
                    Message = ex.Message,
                    Set_Obj = set
                });
            }
        }

        /////Business Partner
        [HttpPost("add_salesemployee")]
        public async Task<IActionResult> add_salesemployee([FromBody] List<tblSalesEmployee> list)
        {
            if (list == null || !list.Any())
            {
                return BadRequest(new ApiResponse
                {
                    Code = 400,
                    Status = "Error",
                    Message = "Invalid Principle Data."
                });
            }
            try
            {
                set = new List<Set_Object>();
                foreach (var x in list)
                {
                    try
                    {
                        var prin = await db.tblSalesEmployees.FirstOrDefaultAsync(a => a.SlpCode == x.SlpCode);
                        var user = await db.Users.FirstOrDefaultAsync(a => a.SlpCode == x.SlpCode);
                        if (prin == null)
                        {
                            db.tblSalesEmployees.Add(new tblSalesEmployee
                            {
                                SlpCode = x.SlpCode,
                                SalesName = x.SalesName,
                                U_SalesCode=x.U_SalesCode,
                                U_Whs = x.U_Whs,
                                SALType=x.SALType,
                                IsAllPrinciple=x.IsAllPrinciple,
                                IsTax=x.IsTax,
                                PrincipleAssign=x.PrincipleAssign,
                                Status="Active",
                                IsDepo=x.IsDepo,
                                DepoID=x.DepoID,
                                MainWhs=x.MainWhs,
                            });
                        }
                        else
                        {
                            prin.SalesName = x.SalesName;
                            prin.U_SalesCode = x.U_SalesCode;
                            prin.U_Whs = x.U_Whs;
                            prin.SALType = x.SALType;
                            prin.IsAllPrinciple = x.IsAllPrinciple;
                            prin.IsTax = x.IsTax;
                            prin.Status = x.Status;
                            prin.PrincipleAssign = x.PrincipleAssign;
                            prin.IsDepo = x.IsDepo;
                            prin.DepoID = x.DepoID;
                            prin.MainWhs = x.MainWhs;
                            db.tblSalesEmployees.Update(prin);
                        }
                        if (user == null)
                        {
                            user = new Models.User();
                            user.Code = x.U_SalesCode;
                            user.Name = x.SalesName;
                            user.IsWebUser = "App";
                            user.IsEndofDay = "No";
                            user.SlpCode = x.SlpCode;
                            user.UserType = "User";
                            user.DeviceID = "";
                            user.CreatedBy = "SAP";
                            user.CreatedDate = DateTime.Now;
                            user.Status = "Inactive";
                            user.Manager = "";
                            db.Users.Add(user);
                        }
                        else
                        {
                            user.Name = x.SalesName;
                            db.Users.Update(user);
                        }
                        await db.SaveChangesAsync();
                        Add_Set("", x.SlpCode.ToString(), "add_salesemployee", "OK");
                    }
                    catch (Exception ex)
                    {
                        Add_Set(ex.Message, x.SlpCode.ToString(), "add_salesemployee", "Failed");
                    }
                }
                return Ok(new ApiResponse
                {
                    Code = 200,
                    Status = "Success",
                    Message = "Sales Employee inserted/updated successfully.",
                    Set_Obj = set
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse
                {
                    Code = 500,
                    Status = "Error",
                    Message = ex.Message
                });
            }
        }

        [HttpPost("add_bp")]
        public async Task<IActionResult> add_bp([FromBody] List<v_Bp> list)
        {
            if (list == null || !list.Any())
            {
                return BadRequest(new ApiResponse
                {
                    Code = 400,
                    Status = "Error",
                    Message = "Invalid Customer Data."
                });
            }
            //using var transaction = await db.Database.BeginTransactionAsync();
            try
            {
                set = new List<Set_Object>();
                foreach (var x in list)
                {
                    try
                    {
                        if (x.ContactList != null && x.ContactList.Any())
                        {
                            foreach (var y in x.ContactList)
                            {
                                var contact = await db.tblBPContacts.FirstOrDefaultAsync(a => a.ContactCode == y.ContactCode);

                                if (contact == null)
                                {
                                    db.tblBPContacts.Add(new tblBPContact
                                    {
                                        ContactCode = y.ContactCode,
                                        ContactName = y.ContactName,
                                        CardCode = y.CardCode,
                                        Tel1 = y.Tel1,
                                        Status = y.Status,
                                    });
                                }
                                else
                                {
                                    contact.CardCode = y.CardCode;
                                    contact.Tel1 = y.Tel1;
                                    contact.ContactName = y.ContactName;
                                    db.tblBPContacts.Update(contact);
                                }
                                await db.SaveChangesAsync();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                    }
                    try
                    {
                        var data = await db.tblBPs.FirstOrDefaultAsync(a => a.CardCode == x.CardCode);
                        if (data == null)
                        {
                            db.tblBPs.Add(new tblBP
                            {
                                CardCode = x.CardCode,
                                CardName = x.CardName,
                                CardFName = x.CardFname,
                                ContactPer = x.ContactPer,
                                GroupCode = x.GroupCode,
                                Phone = x.Phone,
                                VATNo = x.Vatno,
                                DefPriceListCode = x.DefPriceListCode,
                                CreditLimited = x.CreditLimited,
                                TermCode = x.TermCode,
                                TermName = x.TermName,
                                Status = x.Status,
                                GPSLateLong = x.GpslateLong,
                                Balance = x.Balance,
                                Channel = x.Channel,
                                Region = x.Region,
                                ProCode = x.ProCode,
                                DisCode = x.DisCode,
                                ComCode = x.ComCode,
                                VilName = x.VilName,
                                AddressCode = x.AddressCode,
                                FullAddKH = x.FullAddKh,
                                FullAddEN = x.FullAddEn,
                                BPSource = "SAP",
                                SubZone = x.SubZone,
                                Zone = x.Zone,
                                CreatedDate = DateTime.Now
                            });
                        }
                        else
                        {
                            data.CardName = x.CardName;
                            data.CardFName = x.CardFname;
                            data.ContactPer = x.ContactPer;
                            data.GroupCode = x.GroupCode;
                            data.Phone = x.Phone;
                            data.VATNo = x.Vatno;
                            data.DefPriceListCode = x.DefPriceListCode;
                            data.CreditLimited = x.CreditLimited;
                            data.TermCode = x.TermCode;
                            data.TermName = x.TermName;
                            data.Status = x.Status;
                            data.GPSLateLong = x.GpslateLong;
                            data.Balance = x.Balance;
                            data.Channel = x.Channel;
                            data.Region = x.Region;
                            data.ProCode = x.ProCode;
                            data.DisCode = x.DisCode;
                            data.ComCode = x.ComCode;
                            data.VilName = x.VilName;
                            data.AddressCode = x.AddressCode;
                            data.FullAddKH = x.FullAddKh;
                            data.FullAddEN = x.FullAddEn;
                            data.SubZone = x.SubZone;
                            data.Zone = x.Zone;
                            data.UpdatedDate = DateTime.Now;
                            db.tblBPs.Update(data);
                        }
                        await db.SaveChangesAsync();
                        Add_Set("", x.CardCode, "add_bp", "OK");
                    }
                    catch(Exception ex)
                    {
                        Add_Set(ex.Message, x.CardCode, "add_bp", "Failed");
                    }
                }
                
                //await transaction.CommitAsync();
                return Ok(new ApiResponse
                {
                    Code = 200,
                    Status = "Success",
                    Message = "BP inserted/updated successfully.",
                    Set_Obj = set
                });
            }
            catch (Exception ex)
            {
                //await transaction.RollbackAsync();
                return StatusCode(500, new ApiResponse
                {
                    Code = 500,
                    Status = "Error",
                    Message = ex.Message
                });
            }
        }
        [HttpPost("add_bpchannel")]
        public async Task<IActionResult> add_bpchannel([FromBody] List<tblBPChannel> list)
        {
            if (list == null || !list.Any())
            {
                return BadRequest(new ApiResponse
                {
                    Code = 400,
                    Status = "Error",
                    Message = "Invalid Channel Data."
                });
            }
            try
            {
                set = new List<Set_Object>();
                foreach (var x in list)
                {
                    try
                    {
                        var data = await db.tblBPChannels.FirstOrDefaultAsync(a => a.Code == x.Code);

                        if (data == null)
                        {
                            db.tblBPChannels.Add(new tblBPChannel
                            {
                                Code = x.Code,
                                Name = x.Name,
                                Status = x.Status,
                            });
                        }
                        else
                        {
                            if (data.Name != x.Name)
                            {
                                data.Name = x.Name;
                            }
                            if (data.Status != x.Status)
                            {
                                data.Status = x.Status;
                            }
                            db.tblBPChannels.Update(data);
                        }
                        await db.SaveChangesAsync();
                        Add_Set("", x.Code, "add_bpchannel", "OK");
                    }
                    catch(Exception ex) { 
                        Add_Set(ex.Message, x.Code, "add_bpchannel", "Failed");
                    }
                }
                return Ok(new ApiResponse
                {
                    Code = 200,
                    Status = "Success",
                    Message = "BP Channel inserted/updated successfully.",
                    Set_Obj = set
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse
                {
                    Code = 500,
                    Status = "Error",
                    Message = ex.Message
                });
            }
        }
        [HttpPost("add_bpgroup")]
        public async Task<IActionResult> add_bpgroup([FromBody] List<tblBPGroup> list)
        {
            if (list == null || !list.Any())
            {
                return BadRequest(new ApiResponse
                {
                    Code = 400,
                    Status = "Error",
                    Message = "Invalid BP Group Data."
                });
            }
            try
            {
                set = new List<Set_Object>();
                foreach (var x in list)
                {
                    try
                    {
                        var data = await db.tblBPGroups.FirstOrDefaultAsync(a => a.GroupCode == x.GroupCode);
                        if (data == null)
                        {
                            db.tblBPGroups.Add(new tblBPGroup
                            {
                                GroupCode = x.GroupCode,
                                GroupName = x.GroupName,
                                Status = x.Status,
                            });
                        }
                        else
                        {
                            if (data.GroupName != x.GroupName)
                            {
                                data.GroupName = x.GroupName;
                            }
                            if (data.Status != x.Status)
                            {
                                data.Status = x.Status;
                            }
                            db.tblBPGroups.Update(data);
                        }
                        await db.SaveChangesAsync();
                        Add_Set("", x.GroupCode.ToString(), "add_bpgroup", "OK");
                    }
                    catch(Exception ex)
                    {
                        Add_Set(ex.Message, x.GroupCode.ToString(), "add_bpgroup", "Failed");
                    }
                }
                return Ok(new ApiResponse
                {
                    Code = 200,
                    Status = "Success",
                    Message = "BP Group inserted/updated successfully.",
                    Set_Obj= set
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse
                {
                    Code = 500,
                    Status = "Error",
                    Message = ex.Message
                });
            }
        }
        [HttpPost("add_bpmapping")]
        public async Task<IActionResult> add_bpmapping([FromBody] List<tblBPSalMapping> list)
        {
            if (list == null || !list.Any())
            {
                return BadRequest(new ApiResponse
                {
                    Code = 400,
                    Status = "Error",
                    Message = "Invalid BP Sales Mapping Data."
                });
            }
            try
            {
                set = new List<Set_Object>();
                foreach (var x in list)
                {
                    try
                    {
                        var data = await db.tblBPSalMappings.FirstOrDefaultAsync(a => a.Code == x.Code);
                        if (data == null)
                        {
                            db.tblBPSalMappings.Add(new tblBPSalMapping
                            {
                                Code = x.Code,
                                CardCode = x.CardCode,
                                SlpCode = x.SlpCode
                            });
                        }
                        else
                        {
                            if (data.CardCode != x.CardCode)
                            {
                                data.CardCode = x.CardCode;
                            }
                            if (data.SlpCode != x.SlpCode)
                            {
                                data.SlpCode = x.SlpCode;
                            }
                            db.tblBPSalMappings.Update(data);
                        }
                        await db.SaveChangesAsync();
                        Add_Set("", x.Code, "add_bpmapping", "OK");
                    }
                    catch(Exception ex)
                    {
                        Add_Set(ex.Message, x.Code, "add_bpmapping", "Failed");
                    }
                }
                return Ok(new ApiResponse
                {
                    Code = 200,
                    Status = "Success",
                    Message = "BP Sales Mapping inserted/updated successfully.",
                    Set_Obj = set
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse
                {
                    Code = 500,
                    Status = "Error",
                    Message = ex.Message
                });
            }
        }
        [HttpPost("add_term")]
        public async Task<IActionResult> add_term([FromBody] List<tblPayment> list)
        {
            if (list == null || !list.Any())
            {
                return BadRequest(new ApiResponse
                {
                    Code = 400,
                    Status = "Error",
                    Message = "Invalid Term Data."
                });
            }
            try
            {
                set = new List<Set_Object>();
                foreach (var x in list)
                {
                    try
                    {
                        var data = await db.tblPayments.FirstOrDefaultAsync(a => a.TermCode == x.TermCode);

                        if (data == null)
                        {
                            db.tblPayments.Add(new tblPayment
                            {
                                TermCode = x.TermCode,
                                TermName = x.TermName,
                                AddMonth = x.AddMonth,
                                AddDay = x.AddDay,
                                Status = x.Status,
                            });
                        }
                        else
                        {
                            data.TermName = x.TermName;
                            data.AddMonth = x.AddMonth;
                            data.AddDay = x.AddDay;
                            data.Status = x.Status;
                            db.tblPayments.Update(data);
                        }
                        await db.SaveChangesAsync();
                        Add_Set("", x.TermCode.ToString(), "add_term", "OK");
                    }
                    catch(Exception ex)
                    {
                        Add_Set(ex.Message, x.TermCode.ToString(), "add_term", "Failed");
                    }
                }
                return Ok(new ApiResponse
                {
                    Code = 200,
                    Status = "Success",
                    Message = "Term inserted/updated successfully.",
                    Set_Obj = set
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse
                {
                    Code = 500,
                    Status = "Error",
                    Message = ex.Message
                });
            }
        }
        [HttpPost("add_pricelist")]
        public async Task<IActionResult> add_pricelist([FromBody] List<tblPriceList> list)
        {
            if (list == null || !list.Any())
            {
                return BadRequest(new ApiResponse
                {
                    Code = 400,
                    Status = "Error",
                    Message = "Invalid PriceList Data."
                });
            }
            try
            {
                set = new List<Set_Object>();
                foreach (var x in list)
                {
                    try
                    {
                        var data = await db.tblPriceLists.FirstOrDefaultAsync(a => a.ListNum == x.ListNum);

                        if (data == null)
                        {
                            db.tblPriceLists.Add(new tblPriceList
                            {
                                ListNum = x.ListNum,
                                ListName = x.ListName,
                                DocStatus = x.DocStatus,
                                UpdateDate=DateTime.Now
                            });
                        }
                        else
                        {
                            data.ListNum = x.ListNum;
                            data.ListName = x.ListName;
                            data.DocStatus = x.DocStatus;
                            data.UpdateDate = DateTime.Now;
                            db.tblPriceLists.Update(data);
                        }
                        await db.SaveChangesAsync();
                        Add_Set("", x.ListNum.ToString(), "add_pricelist", "OK");
                    }
                    catch (Exception ex)
                    {
                        Add_Set(ex.Message, x.ListNum.ToString(), "add_pricelist", "Failed");
                    }
                }
                return Ok(new ApiResponse
                {
                    Code = 200,
                    Status = "Success",
                    Message = "PriceList inserted/updated successfully.",
                    Set_Obj = set
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse
                {
                    Code = 500,
                    Status = "Error",
                    Message = ex.Message
                });
            }
        }

        [HttpPost("add_address")]
        public async Task<IActionResult> add_address([FromBody] List<tblAddress> list)
        {
            if (list == null || !list.Any())
            {
                return BadRequest(new ApiResponse
                {
                    Code = 400,
                    Status = "Error",
                    Message = "Invalid Address Data."
                });
            }
            try
            {
                set = new List<Set_Object>();
                foreach (var x in list)
                {
                    try
                    {
                        var data = await db.tblAddresses.FirstOrDefaultAsync(a => a.AddressCode == x.AddressCode);

                        if (data == null)
                        {
                            db.tblAddresses.Add(new tblAddress
                            {
                                AddressCode = x.AddressCode,
                                AddressName = x.AddressName,
                                ComCode = x.ComCode,
                                ComENName = x.ComENName,
                                ComKHName = x.ComKHName,
                                DisCode = x.DisCode,
                                DisENName = x.DisENName,
                                DisKHName = x.DisKHName,
                                ProCode = x.ProCode,
                                ProENName = x.ProENName,
                                ProKHName = x.ProKHName,
                                AddressEN = x.AddressEN,
                                AddressKH = x.AddressKH,
                                Zone = x.Zone,
                                SubZone = x.SubZone
                            });
                        }
                        else
                        {
                            data.AddressName = x.AddressName;
                            data.ComCode = x.ComCode;
                            data.ComENName = x.ComENName;
                            data.ComKHName = x.Status;
                            data.DisCode = x.DisCode;
                            data.DisENName = x.DisENName;
                            data.DisKHName = x.DisKHName;
                            data.ProCode = x.ProCode;
                            data.ProENName = x.ProENName;
                            data.ProKHName = x.ProKHName;
                            data.AddressEN = x.AddressEN;
                            data.AddressKH = x.AddressKH;
                            data.Zone = x.Zone;
                            data.SubZone = x.SubZone;
                            db.tblAddresses.Update(data);
                        }
                        await db.SaveChangesAsync();
                        Add_Set("", x.AddressCode.ToString(), "add_address", "OK");
                    }
                    catch(Exception ex)
                    {
                        Add_Set(ex.Message, x.AddressCode.ToString(), "add_address", "Failed");
                    }
                }
                

                return Ok(new ApiResponse
                {
                    Code = 200,
                    Status = "Success",
                    Message = "Address inserted/updated successfully.",
                    Set_Obj = set
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse
                {
                    Code = 500,
                    Status = "Error",
                    Message = ex.Message
                });
            }
        }
        [HttpPost("add_sal_mapping")]
        public async Task<IActionResult> add_sal_mapping([FromBody] List<v_salmapping> list)
        {
            if (list == null || !list.Any())
            {
                return BadRequest(new ApiResponse
                {
                    Code = 400,
                    Status = "Error",
                    Message = "Invalid Sales Mapping Data."
                });
            }
            try
            {
                set = new List<Set_Object>();
                foreach (var x in list)
                {
                    try
                    {
                        //For Item Sales Man
                        foreach(var s in x.listsalitems)
                        {
                            var item = await db.tblItemSalesMen.Where(a => a.DocEntry == s.DocEntry && a.LineNum==s.LineNum  && a.SlpCode==x.slpcode ).FirstOrDefaultAsync();
                            if (item == null)
                            {
                                db.tblItemSalesMen.Add(new tblItemSalesMan() { 
                                    DocEntry=s.DocEntry,
                                    LineNum=s.LineNum,  
                                    DocStatus=s.DocStatus,  
                                    ItemCode=s.ItemCode,    
                                    SlpCode=s.SlpCode,
                                    Sync="Pending",
                                });
                            }
                            else
                            {
                                item.DocStatus=s.DocStatus;
                                item.ItemCode=s.ItemCode;
                                item.SlpCode = s.SlpCode;
                                db.tblItemSalesMen.Update(item);
                            }
                        }
                        // For Sales Region
                        foreach (var s in x.listsalregions)
                        {
                            var item = await db.tblSalesEmployee1s.Where(a => a.DocEntry == s.DocEntry && a.LineNum == s.LineNum && a.SlpCode == x.slpcode).FirstOrDefaultAsync();
                            if (item == null)
                            {
                                db.tblSalesEmployee1s.Add(new tblSalesEmployee1()
                                {
                                    DocEntry = s.DocEntry,
                                    LineNum = s.LineNum,
                                    DocStatus = s.DocStatus,
                                    Region = s.Region,
                                    SlpCode = s.SlpCode
                                });
                            }
                            else
                            {
                                item.DocStatus = s.DocStatus;
                                item.Region = s.Region;
                                item.SlpCode = s.SlpCode;
                                db.tblSalesEmployee1s.Update(item);
                            }
                        }
                        await db.SaveChangesAsync();
                        Add_Set("", x.slpcode.ToString(), "add_sal_mapping", "OK");
                    }
                    catch (Exception ex)
                    {
                        Add_Set(ex.Message, x.slpcode.ToString(), "add_sal_mapping", "Failed");
                    }
                }
                return Ok(new ApiResponse
                {
                    Code = 200,
                    Status = "Success",
                    Message = "Sales Mapping inserted/updated successfully.",
                    Set_Obj = set
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse
                {
                    Code = 500,
                    Status = "Error",
                    Message = ex.Message
                });
            }
        }
        [HttpPost("add_Zone")]
        public async Task<IActionResult> add_Zone([FromBody] List<tblZone> list)
        {
            if (list == null || !list.Any())
            {
                return BadRequest(new ApiResponse
                {
                    Code = 400,
                    Status = "Error",
                    Message = "Invalid Zone Data."
                });
            }
            try
            {
                set = new List<Set_Object>();
                foreach (var x in list)
                {
                    try
                    {
                        var data = await db.tblZones.FirstOrDefaultAsync(a => a.Code == x.Code);

                        if (data == null)
                        {
                            db.tblZones.Add(new tblZone
                            {
                                Code = x.Code,
                                ZoneName = x.ZoneName,
                                DocStatus = x.DocStatus,
                                CreatedDate = DateTime.Now
                            });
                        }
                        else
                        {
                            data.ZoneName = x.ZoneName;
                            data.DocStatus = x.DocStatus;
                            db.tblZones.Update(data);
                        }
                        await db.SaveChangesAsync();
                        Add_Set("", x.Code, "add_Zone", "OK");
                    }
                    catch(Exception ex)
                    {
                        Add_Set(ex.Message, x.Code, "add_Zone", "Failed");
                    }
                }
                return Ok(new ApiResponse
                {
                    Code = 200,
                    Status = "Success",
                    Message = "Zone inserted/updated successfully.",
                    Set_Obj = set
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse
                {
                    Code = 500,
                    Status = "Error",
                    Message = ex.Message
                });
            }
        }

        [HttpPost("add_SubZone")]
        public async Task<IActionResult> add_SubZone([FromBody] List<tblSubZone> list)
        {
            if (list == null || !list.Any())
            {
                return BadRequest(new ApiResponse
                {
                    Code = 400,
                    Status = "Error",
                    Message = "Invalid Sub Zone Data."
                });
            }
            try
            {
                set = new List<Set_Object>();
                foreach (var x in list)
                {
                    try
                    {
                        var data = await db.tblSubZones.FirstOrDefaultAsync(a => a.Code == x.Code);
                        if (data == null)
                        {
                            db.tblSubZones.Add(new tblSubZone
                            {
                                Code = x.Code,
                                SubZoneName = x.SubZoneName,
                                DocStatus = x.DocStatus,
                                CreatedDate = DateTime.Now
                            });
                        }
                        else
                        {
                            data.SubZoneName = x.SubZoneName;
                            data.DocStatus = x.DocStatus;
                            db.tblSubZones.Update(data);
                        }
                        await db.SaveChangesAsync();
                        Add_Set("", x.Code, "add_SubZone", "OK");
                    }
                    catch(Exception ex)
                    {
                        Add_Set(ex.Message, x.Code, "add_SubZone", "Failed");
                    }
                }
                return Ok(new ApiResponse
                {
                    Code = 200,
                    Status = "Success",
                    Message = "Sub Zone inserted/updated successfully.",
                    Set_Obj = set
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse
                {
                    Code = 500,
                    Status = "Error",
                    Message = ex.Message
                });
            }
        }

        /////Promotion
        [HttpPost("add_prom")]
        public async Task<IActionResult> add_prom([FromBody] List<tblPromotion> list)
        {
            if (list == null || !list.Any())
            {
                return BadRequest(new ApiResponse
                {
                    Code = 400,
                    Status = "Error",
                    Message = "Invalid Promotion Head Data."
                });
            }
            try
            {
                set = new List<Set_Object>();
                foreach (var x in list)
                {
                    try
                    {
                        var data = await db.tblPromotions.FirstOrDefaultAsync(a => a.ProEntry == x.ProEntry);

                        if (data == null)
                        {
                            db.tblPromotions.Add(new tblPromotion
                            {
                                ProEntry = x.ProEntry,
                                PrincipleCode = x.PrincipleCode,
                                PrincipleDesc = x.PrincipleDesc,
                                DocStatus = x.DocStatus,
                                FDate = x.FDate,
                                TDate = x.TDate,
                                CreatedDate = DateTime.Now
                            });
                        }
                        else
                        {
                            data.PrincipleCode = x.PrincipleCode;
                            data.PrincipleDesc = x.PrincipleDesc;
                            data.DocStatus = x.DocStatus;
                            data.FDate = x.FDate;
                            data.TDate = x.TDate;
                            data.CreatedDate = DateTime.Now;
                            db.tblPromotions.Update(data);
                        }
                        await db.SaveChangesAsync();
                        Add_Set("", x.ProEntry.ToString(), "add_prom", "OK");
                    }
                    catch(Exception ex)
                    {
                        Add_Set(ex.Message, x.ProEntry.ToString(), "add_prom", "Failed");
                    }
                }
                return Ok(new ApiResponse
                {
                    Code = 200,
                    Status = "Success",
                    Message = "Promotion inserted/updated successfully.",
                    Set_Obj = set
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse
                {
                    Code = 500,
                    Status = "Error",
                    Message = ex.Message
                });
            }
        }
        [HttpPost("add_prom1")]
        public async Task<IActionResult> add_prom1([FromBody] List<tblPromotion1> list)
        {
            if (list == null || !list.Any())
            {
                return BadRequest(new ApiResponse
                {
                    Code = 400,
                    Status = "Error",
                    Message = "Invalid Promotion Head Data."
                });
            }
            try
            {
                set = new List<Set_Object>();
                foreach (var x in list)
                {
                    try
                    {
                        var data = await db.tblPromotion1s.FirstOrDefaultAsync(a => a.ProEntry == x.ProEntry && a.LineNum == x.LineNum);

                        if (data == null)
                        {
                            db.tblPromotion1s.Add(new tblPromotion1
                            {
                                ProEntry = x.ProEntry,
                                LineNum = x.LineNum,
                                ItemGroupCode = x.ItemGroupCode,
                                ItemGroupName = x.ItemGroupName,
                                PackType = x.PackType,
                                PromotionGroup = x.PromotionGroup,
                                BPGroup = x.BPGroup,
                                BPGroupName = x.BPGroupName,
                                BPChannelCode = x.BPChannelCode,
                                BPChannelName = x.BPChannelName,
                                CardCode = x.CardCode,
                                CardName = x.CardName,
                                Combine = x.Combine,
                                PromotionType = x.PromotionType,
                                FOCType = x.FOCType,
                                Condition = x.Condition,
                                BuyAmt = x.BuyAmt,
                                BuyQty = x.BuyQty,
                                BuyUoM = x.BuyUoM,
                                FOCQty = x.FOCQty,
                                FOCUoM = x.FOCUoM,
                                DisPer = x.DisPer,
                                DisAmt = x.DisAmt,
                                Remark = x.Remark,
                                ValidFrom = x.ValidFrom,
                                ValidTo = x.ValidTo,
                                LineStatus = x.LineStatus,
                            });
                        }
                        else
                        {
                            data.ItemGroupCode = x.ItemGroupCode;
                            data.ItemGroupName = x.ItemGroupName;
                            data.PackType = x.PackType;
                            data.PromotionGroup = x.PromotionGroup;
                            data.BPGroup = x.BPGroup;
                            data.BPGroupName = x.BPGroupName;
                            data.BPChannelCode = x.BPChannelCode;
                            data.BPChannelName = x.BPChannelName;
                            data.CardCode = x.CardCode;
                            data.CardName = x.CardName;
                            data.Combine = x.Combine;
                            data.PromotionType = x.PromotionType;
                            data.FOCType = x.FOCType;
                            data.Condition = x.Condition;
                            data.BuyAmt = x.BuyAmt;
                            data.BuyQty = x.BuyQty;
                            data.BuyUoM = x.BuyUoM;
                            data.FOCQty = x.FOCQty;
                            data.FOCUoM = x.FOCUoM;
                            data.DisPer = x.DisPer;
                            data.DisAmt = x.DisAmt;
                            data.Remark = x.Remark;
                            data.ValidFrom = x.ValidFrom;
                            data.ValidTo = x.ValidTo;
                            data.LineStatus = x.LineStatus;
                            db.tblPromotion1s.Update(data);
                        }
                        await db.SaveChangesAsync();
                        Add_Set("", x.ProEntry.ToString() + "-" + x.LineNum.ToString(), "add_prom1", "OK");
                    }
                    catch(Exception ex)
                    {
                        Add_Set(ex.Message, x.ProEntry.ToString() + "-" + x.LineNum.ToString(), "add_prom1", "Failed");
                    }
                }
                return Ok(new ApiResponse
                {
                    Code = 200,
                    Status = "Success",
                    Message = "Promotion 1 inserted/updated successfully.",
                    Set_Obj = set
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse
                {
                    Code = 500,
                    Status = "Error",
                    Message = ex.Message
                });
            }
        }

        [HttpPost("add_promgroup")]
        public async Task<IActionResult> add_promgroup([FromBody] List<tblPromotionGroup> list)
        {
            if (list == null || !list.Any())
            {
                return BadRequest(new ApiResponse
                {
                    Code = 400,
                    Status = "Error",
                    Message = "Invalid Promotion Group Data."
                });
            }
            try
            {
                set = new List<Set_Object>();
                foreach (var x in list)
                {
                    try
                    {
                        var data = await db.tblPromotionGroups.FirstOrDefaultAsync(a => a.ProGroupCode == x.ProGroupCode);

                        if (data == null)
                        {
                            db.tblPromotionGroups.Add(new tblPromotionGroup
                            {
                                ProGroupCode = x.ProGroupCode,
                                ProDesc = x.ProDesc,
                                ProStatus = x.ProStatus,
                                CreatedDate = DateTime.Now
                            });
                        }
                        else
                        {
                            data.ProDesc = x.ProDesc;
                            data.ProStatus = x.ProStatus;
                            data.CreatedDate = DateTime.Now;
                            db.tblPromotionGroups.Update(data);
                        }
                        await db.SaveChangesAsync();
                        Add_Set("", x.ProGroupCode, "add_promgroup", "OK");
                    }
                    catch(Exception ex)
                    {
                        Add_Set(ex.Message, x.ProGroupCode, "add_promgroup", "Failed");
                    }
                }
                return Ok(new ApiResponse
                {
                    Code = 200,
                    Status = "Success",
                    Message = "Promotion Group inserted/updated successfully.",
                    Set_Obj = set
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse
                {
                    Code = 500,
                    Status = "Error",
                    Message = ex.Message
                });
            }
        }
        //[HttpPost("add_promtype")]
        //public async Task<IActionResult> add_promtype([FromBody] List<TblProType> list)
        //{
        //    if (list == null || !list.Any())
        //    {
        //        return BadRequest(new ApiResponse
        //        {
        //            Code = 400,
        //            Status = "Error",
        //            Message = "Invalid Promotion Type Data."
        //        });
        //    }
        //    try
        //    {
        //        foreach (var x in list)
        //        {
        //            var data = await db.TblProTypes.FirstOrDefaultAsync(a => a.ProTypeCode == x.ProTypeCode);

        //            if (data == null)
        //            {
        //                db.TblProTypes.Add(new TblProType
        //                {
        //                    ProTypeCode = x.ProTypeCode,
                           
        //                    DocStatus = x.DocStatus,
        //                    CreatedDate = DateTime.Now
        //                });
        //            }
        //            else
        //            {
        //                if (data.ProTypeDesc != x.ProTypeDesc)
        //                {
        //                    data.ProTypeDesc = x.ProTypeDesc;
        //                }
        //                if (data.DocStatus != x.DocStatus)
        //                {
        //                    data.DocStatus = x.DocStatus;
        //                }
        //                data.UpdateDate = DateTime.Now;
        //                db.TblProTypes.Update(data);
        //            }
        //        }
        //        await db.SaveChangesAsync();

        //        return Ok(new ApiResponse
        //        {
        //            Code = 200,
        //            Status = "Success",
        //            Message = "Promotion Type inserted/updated successfully."
        //        });
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, new ApiResponse
        //        {
        //            Code = 500,
        //            Status = "Error",
        //            Message = ex.Message
        //        });
        //    }
        //}

        [HttpPost("add_promfoctype")]
        public async Task<IActionResult> add_promfoctype([FromBody] List<tblProFOCType> list)
        {
            if (list == null || !list.Any())
            {
                return BadRequest(new ApiResponse
                {
                    Code = 400,
                    Status = "Error",
                    Message = "Invalid FOC Type Data."
                });
            }
            try
            {
                set = new List<Set_Object>();
                foreach (var x in list)
                {
                    try
                    {
                        var data = await db.tblProFOCTypes.FirstOrDefaultAsync(a => a.Code == x.Code);

                        if (data == null)
                        {
                            db.tblProFOCTypes.Add(new tblProFOCType
                            {
                                Code = x.Code,
                                Dscription = x.Dscription,
                                DocStatus = x.DocStatus,
                                CreatedDate = DateTime.Now
                            });
                        }
                        else
                        {
                            if (data.Dscription != x.Dscription)
                            {
                                data.Dscription = x.Dscription;
                            }
                            if (data.DocStatus != x.DocStatus)
                            {
                                data.DocStatus = x.DocStatus;
                            }
                            data.UpdateDate = DateTime.Now;
                            db.tblProFOCTypes.Update(data);
                        }
                        await db.SaveChangesAsync();
                        Add_Set("", x.Code, "add_promfoctype", "OK");
                    }
                    catch(Exception ex)
                    {
                        Add_Set(ex.Message, x.Code, "add_promfoctype", "Failed");
                    }
                }
                return Ok(new ApiResponse
                {
                    Code = 200,
                    Status = "Success",
                    Message = "Promotion FOC Type inserted/updated successfully.",
                    Set_Obj = set
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse
                {
                    Code = 500,
                    Status = "Error",
                    Message = ex.Message
                });
            }
        }
        //[HttpPost("add_promcondition")]
        //public async Task<IActionResult> add_promcondition([FromBody] List<TblProCondition> list)
        //{
        //    if (list == null || !list.Any())
        //    {
        //        return BadRequest(new ApiResponse
        //        {
        //            Code = 400,
        //            Status = "Error",
        //            Message = "Invalid Promotion Condition Data."
        //        });
        //    }
        //    try
        //    {
        //        foreach (var x in list)
        //        {
        //            var data = await db.TblProConditions.FirstOrDefaultAsync(a => a.ConditionCode == x.ConditionCode);

        //            if (data == null)
        //            {
        //                db.TblProConditions.Add(new TblProCondition
        //                {
        //                    ConditionCode = x.ConditionCode,
        //                    Dscription = x.Dscription,
        //                    DocStatus = x.DocStatus,
        //                    CreatedDate = DateTime.Now
        //                });
        //            }
        //            else
        //            {
        //                if (data.Dscription != x.Dscription)
        //                {
        //                    data.Dscription = x.Dscription;
        //                }
        //                if (data.DocStatus != x.DocStatus)
        //                {
        //                    data.DocStatus = x.DocStatus;
        //                }
        //                data.UpdateDate = DateTime.Now;
        //                db.TblProConditions.Update(data);
        //            }
        //        }
        //        await db.SaveChangesAsync();

        //        return Ok(new ApiResponse
        //        {
        //            Code = 200,
        //            Status = "Success",
        //            Message = "Promotion Promotion Condition inserted/updated successfully."
        //        });
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, new ApiResponse
        //        {
        //            Code = 500,
        //            Status = "Error",
        //            Message = ex.Message
        //        });
        //    }
        //}

        /////Bank
        [HttpPost("add_bank")]
        public async Task<IActionResult> add_bank([FromBody] List<tblBank> list)
        {
            if (list == null || !list.Any())
            {
                return BadRequest(new ApiResponse
                {
                    Code = 400,
                    Status = "Error",
                    Message = "Invalid Bank Data."
                });
            }
            try
            {
                set = new List<Set_Object>();
                foreach (var x in list)
                {
                    try
                    {
                        var data = await db.tblBanks.FirstOrDefaultAsync(a => a.BankCode == x.BankCode);

                        if (data == null)
                        {
                            db.tblBanks.Add(new tblBank
                            {
                                Code = x.Code,
                                BankCode = x.BankCode,
                                BankName = x.BankName,
                                GLAccount = x.GLAccount,
                                CurCode = x.CurCode,
                            });
                        }
                        else
                        {
                            data.BankCode = x.BankCode;
                            data.BankName = x.BankName;
                            data.GLAccount = x.GLAccount;
                            data.CurCode = x.CurCode;
                            db.tblBanks.Update(data);
                        }
                        await db.SaveChangesAsync();
                        Add_Set("", x.BankCode, "add_bank", "OK");
                    }
                    catch(Exception ex)
                    {
                        Add_Set(ex.Message, x.BankCode, "add_bank", "Failed");
                    }
                }
                return Ok(new ApiResponse
                {
                    Code = 200,
                    Status = "Success",
                    Message = "Bank inserted/updated successfully.",
                    Set_Obj = set
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse
                {
                    Code = 500,
                    Status = "Error",
                    Message = ex.Message
                });
            }
        }
        ///ExChange Rate
        [HttpPost("add_exchange")]
        public async Task<IActionResult> add_exchange([FromBody] List<ExchangeRate> list)
        {
            if (list == null || !list.Any())
            {
                return BadRequest(new ApiResponse
                {
                    Code = 400,
                    Status = "Error",
                    Message = "Invalid Rate."
                });
            }
            try
            {
                foreach(var x in list)
                {
                    var data = await db.ExchangeRates.FirstOrDefaultAsync(a => a.RateDate == x.RateDate && a.CurCode==x.CurCode);
                    if (data == null)
                    {
                        db.ExchangeRates.Add(new ExchangeRate
                        {
                            CurCode = x.CurCode,
                            Amount = x.Amount,
                            RateDate = x.RateDate,
                            CreatedDate = DateTime.Now
                        });
                    }
                    else
                    {
                        if (data.CurCode == x.CurCode && data.Amount != x.Amount)
                        {
                            data.Amount = x.Amount;
                        }
                    }
                    await db.SaveChangesAsync();
                }
                
                return Ok(new ApiResponse
                {
                    Code = 200,
                    Status = "Success",
                    Message = "Exchange Rate inserted/updated successfully."
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse
                {
                    Code = 500,
                    Status = "Error",
                    Message = ex.Message
                });
            }
        }

        [HttpGet("Get_Available_Order")]
        public async Task<ActionResult<List<v_Order>>> Get_Available_Order()
        {
            try
            {
                var order = db.Get_Order_Available_Results.FromSqlRaw("EXEC dbo.ICC_Api_SAP_Get_Available_Order").AsEnumerable().ToList();
                if (order == null)
                    return NotFound();
                foreach(var x in order)
                {
                    x.order1 = db.Get_Order_Available1_Results.FromSqlRaw("EXEC dbo.ICC_Api_SAP_Get_Order1 @p0", x.DocEntry).AsEnumerable().ToList();
                }
                return Ok(order);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    error = "An error occurred while retrieving available orders.",
                    details = ex.Message
                });
            }
        }

        [HttpGet("Get_Available_Income")]
        public async Task<ActionResult<List<v_Income>>> Get_Available_Income()
        {
            try
            {
                var income = db.v_IncomeResults.FromSqlRaw("EXEC dbo.ICC_Api_SAP_Get_Available_Income").AsEnumerable().ToList();
                return Ok(income);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    error = "An error occurred while retrieving available orders.",
                    details = ex.Message
                });
            }
        }
        [HttpGet("Get_Available_BP_Request")]
        public async Task<ActionResult<List<tblBPRequest>>> Get_Available_BP_Request()
        {
            try
            {
                var bp = await db.Api_SAP_Get_Available_BPRequest_Results
                    .FromSqlRaw("EXEC dbo.Api_SAP_Get_Available_BPRequest")
                    .ToListAsync();
                return Ok(bp);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    error = "An error occurred while retrieving available BP requests.",
                    details = ex.Message
                });
            }
        }



        [HttpPost("Set_Order")]
        public async Task<IActionResult> Set_Order([FromBody] v_Order order)
        {
            if (order == null)
            {
                return BadRequest(new ApiResponse
                {
                    Code = 400,
                    Status = "Error",
                    Message = "Invalid Order Data."
                });
            }
            try
            {
                //db.ICC_Api_SAP_Set_Order(order.DocEntry, Convert.ToInt32(order.AppId), order.Remark);
                SO so = db.SOs.Where(a => a.DocEntry == order.DocEntry).FirstOrDefault();
                if (so != null)
                {
                    if (order.AppId == -1)
                    {
                        so.SAPSyncStatus = "Failed";
                        so.SAPLastError = order.Remark;
                    }
                    else
                    {
                        so.SAPDocEntry = order.AppId;
                        so.SAPDocNum = order.AppDocNo;
                        so.SAPSyncStatus = "Completed";
                        so.DocStatus = "Syned";
                        so.AppStatus = "Yes";
                        so.SAPLastError = null; // optional cleanup
                        //DocEntry Mapping
                        DocEntryMapping? mapping =await db.DocEntryMappings.Where(x => x.DocType == "SO" && x.DMSEntry == order.DocEntry).FirstOrDefaultAsync();
                        if (mapping != null)
                        {
                            mapping.SAPEntry = order.AppId;
                        }
                    }
                    db.SOs.Update(so);  // ✅ Pass the entity you updated
                    db.SaveChanges();   // ✅ Commit the changes to database
                }
                return Ok(new ApiResponse
                {
                    Code = 200,
                    Status = "Success",
                    Message = "Order updated successfully."
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse
                {
                    Code = 500,
                    Status = "Error",
                    Message = ex.Message
                });
            }
        }
        [HttpPost("Set_Income")]
        public async Task<IActionResult> Set_Income([FromBody] v_Income income)
        {
            if (income == null)
            {
                return BadRequest(new ApiResponse
                {
                    Code = 400,
                    Status = "Error",
                    Message = "Invalid Income Data."
                });
            }
            try
            {
                tblIncome so = db.tblIncomes.Where(a => a.DocEntry == income.DocEntry).FirstOrDefault();
                if (so != null)
                {
                    if (income.SAPDocEntry == -1)
                    {
                        so.IntegrationStatus = "Failed";
                        so.LastError = income.LastError;
                    }
                    else
                    {
                        so.SAPIncomeDocEntry = income.SAPDocEntry;
                        so.IntegrationStatus = "Completed";
                        so.LastError = null; // optional cleanup
                    }
                    db.tblIncomes.Update(so);  // ✅ Pass the entity you updated
                    db.SaveChanges();   // ✅ Commit the changes to database
                }
                return Ok(new ApiResponse
                {
                    Code = 200,
                    Status = "Success",
                    Message = "Income updated successfully."
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse
                {
                    Code = 500,
                    Status = "Error",
                    Message = ex.Message
                });
            }
        }
        [HttpPost("Set_BPRequest")]
        public async Task<IActionResult> Set_BPRequest([FromBody] tblBPRequest bp)
        {
            if (bp == null)
            {
                return BadRequest(new ApiResponse
                {
                    Code = 400,
                    Status = "Error",
                    Message = "Invalid BP Request Data."
                });
            }
            try
            {
                tblBPRequest? bPRequest = await db.tblBPRequests.Where(a => a.DocEntry == bp.DocEntry).FirstOrDefaultAsync();
                if (bPRequest != null)
                {
                    if (bp.CardCode == null || bp.CardCode=="" )
                    {
                        bPRequest.SAPSyncStatus = "Failed";
                        bPRequest.LastError = bp.LastError;
                    }
                    else
                    {
                        bPRequest.CardCode = bp.CardCode;
                        bPRequest.SAPSyncStatus = "Success";
                        bPRequest.LastError = null; // optional cleanup
                        //DocEntry Mapping
                        DocEntryMapping? mapping = await db.DocEntryMappings.Where(x => x.DocType == "BPRequest" && x.DMSEntry == bPRequest.DocEntry).FirstOrDefaultAsync();
                        if (mapping != null)
                        {
                            mapping.SAPEntry = -1;
                        }
                    }
                    db.tblBPRequests.Update(bPRequest);  // ✅ Pass the entity you updated
                    db.SaveChanges();   // ✅ Commit the changes to database
                }
                return Ok(new ApiResponse
                {
                    Code = 200,
                    Status = "Success",
                    Message = "BP Request updated successfully."
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse
                {
                    Code = 500,
                    Status = "Error",
                    Message = ex.Message
                });
            }
        }

        [HttpPost("Set_Order_Status")]
        public async Task<IActionResult> Set_Order_Status([FromBody] v_Order_Status orderstatus)
        {
            if (orderstatus == null)
            {
                return BadRequest(new ApiResponse
                {
                    Code = 400,
                    Status = "Error",
                    Message = "Invalid Order Status."
                });
            }
            try
            {
                set = new List<Set_Object>();
                try
                {
                    SO so = db.SOs.Where(a => a.DocEntry == orderstatus.DocEntry).FirstOrDefault();
                    if (so != null)
                    {
                        so.DocStatus = orderstatus.DocStatus;
                        so.UpdateDate = DateTime.Now;
                        so.AppStatus = "Yes";
                        db.SOs.Update(so);
                        db.SaveChanges();
                        Add_Set("", orderstatus.DocEntry.ToString(), "Set_Order_Status", "OK");
                    }
                }
                catch (Exception ex)
                {
                    Add_Set(ex.Message, orderstatus.DocEntry.ToString(), "Set_Order_Status", "Failed");
                }
                return Ok(new ApiResponse
                {
                    Code = 200,
                    Status = "Success",
                    Message = "Order updated Status successfully.",
                    Set_Obj = set
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse
                {
                    Code = 500,
                    Status = "Error",
                    Message = ex.Message
                });
            }
        }

        [HttpPost("add_item_batch")]
        public async Task<IActionResult> add_item_batch([FromBody] List<TblItemBatch> list)
        {
            if (list == null || !list.Any())
            {
                return BadRequest(new ApiResponse
                {
                    Code = 400,
                    Status = "Error",
                    Message = "Invalid Batch data."
                });
            }
            try
            {
                set = new List<Set_Object>();
                foreach (var x in list)
                {
                    // Check if AbsEntry exists
                    try
                    {
                        var existing = await db.tblItemBatchs.FirstOrDefaultAsync(a => a.AbsEntry == x.AbsEntry);
                        if (existing == null)
                        {
                            // Insert new record
                            x.UpdatedDate = DateTime.Now;
                            db.tblItemBatchs.Add(x);
                        }
                        else
                        {
                            // Update existing record
                            existing.ItemCode = x.ItemCode;
                            existing.BatchNum = x.BatchNum;
                            existing.InDate = x.InDate;
                            existing.ExpDate = x.ExpDate;
                            existing.MnfDate = x.MnfDate;
                            existing.WhsCode = x.WhsCode;
                            existing.BatchQty = x.BatchQty;
                            existing.UpdatedDate = DateTime.Now;
                        }
                        Add_Set("", x.AbsEntry.ToString(), "add_item_batch", "OK");
                    }
                    catch(Exception ex)
                    {
                        Add_Set(ex.Message, x.AbsEntry.ToString(), "add_item_batch", "Failed");
                    }
                }
                // Save all changes at once (better performance than inside loop)
                await db.SaveChangesAsync();
                return Ok(new ApiResponse
                {
                    Code = 200,
                    Status = "Success",
                    Message = "Item batches inserted/updated successfully.",
                    Set_Obj = set
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse
                {
                    Code = 500,
                    Status = "Error",
                    Message = ex.Message
                });
            }
        }
        [HttpPost("add_item_whs_price")]
        public async Task<IActionResult> add_item_whs_price([FromBody] List<tblItemWhsPricing> list)
        {
            if (list == null || !list.Any())
            {
                return BadRequest(new ApiResponse
                {
                    Code = 400,
                    Status = "Error",
                    Message = "Invalid Item Warehouse Pricing."
                });
            }

            try
            {
                set = new List<Set_Object>();

                foreach (var x in list)
                {
                    try
                    {
                        var existing = await db.tblItemWhsPricings
                            .FirstOrDefaultAsync(a => a.Code == x.ItemCode + "-" + x.WhsCode);

                        if (existing == null)
                        {
                            var newItem = new tblItemWhsPricing
                            {
                                Code = x.ItemCode + "-" + x.WhsCode,
                                ItemCode = x.ItemCode,
                                WhsCode = x.WhsCode,
                                UoMEntry = x.UoMEntry,
                                Amount = x.Amount
                            };
                            db.tblItemWhsPricings.Add(newItem);
                        }
                        else
                        {
                            if (existing.Amount != x.Amount)
                            {
                                existing.Amount = x.Amount;
                                db.tblItemWhsPricings.Update(existing);
                            }
                        }
                        Add_Set("", x.ItemCode + "-" + x.WhsCode, "add_item_whs_price", "OK");
                    }
                    catch (Exception ex)
                    {
                        Add_Set(ex.Message, x.ItemCode + "-" + x.WhsCode, "add_item_whs_price", "Failed");
                    }
                }

                await db.SaveChangesAsync();

                return Ok(new ApiResponse
                {
                    Code = 200,
                    Status = "Success",
                    Message = "Item Price Warehouse inserted/updated successfully.",
                    Set_Obj = set
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse
                {
                    Code = 500,
                    Status = "Error",
                    Message = ex.Message
                });
            }
        }
        [HttpPost("add_ar")]
        public async Task<IActionResult> add_ar([FromBody] List<tblItemWhsPricing> list)
        {
            if (list == null || !list.Any())
            {
                return BadRequest(new ApiResponse
                {
                    Code = 400,
                    Status = "Error",
                    Message = "Invalid Item Warehouse Pricing."
                });
            }

            try
            {
                set = new List<Set_Object>();

                foreach (var x in list)
                {
                    try
                    {
                        var existing = await db.tblItemWhsPricings
                            .FirstOrDefaultAsync(a => a.Code == x.ItemCode + "-" + x.WhsCode);

                        if (existing == null)
                        {
                            var newItem = new tblItemWhsPricing
                            {
                                Code = x.ItemCode + "-" + x.WhsCode,
                                ItemCode = x.ItemCode,
                                WhsCode = x.WhsCode,
                                UoMEntry = x.UoMEntry,
                                Amount = x.Amount
                            };
                            db.tblItemWhsPricings.Add(newItem);
                        }
                        else
                        {
                            if (existing.Amount != x.Amount)
                            {
                                existing.Amount = x.Amount;
                                db.tblItemWhsPricings.Update(existing);
                            }
                        }
                        Add_Set("", x.ItemCode + "-" + x.WhsCode, "add_item_whs_price", "OK");
                    }
                    catch (Exception ex)
                    {
                        Add_Set(ex.Message, x.ItemCode + "-" + x.WhsCode, "add_item_whs_price", "Failed");
                    }
                }

                await db.SaveChangesAsync();

                return Ok(new ApiResponse
                {
                    Code = 200,
                    Status = "Success",
                    Message = "Item Price Warehouse inserted/updated successfully.",
                    Set_Obj = set
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse
                {
                    Code = 500,
                    Status = "Error",
                    Message = ex.Message
                });
            }
        }

        //[HttpPut("update_dn_status")]
        //public async Task<IActionResult> UpdateDNStatus(List<SO> models)
        //{
        //    if (models == null || !models.Any())
        //    {
        //        return BadRequest("Request data is empty");
        //    }

        //    try
        //    {
        //        foreach (var x in models)
        //        {
        //            var so = await db.SOs
        //                .FirstOrDefaultAsync(a => a.SAPDocEntry == x.SAPDocEntry);

        //            if (so == null)
        //                continue; // skip if not found

        //            so.SAPSyncStatus = x.SAPSyncStatus;
        //        }

        //        await db.SaveChangesAsync(); // ✅ Save once only

        //        return Ok(new ApiResponse
        //        {
        //            Code = 200,
        //            Status = "Success",
        //            Message = "SAP sync status updated successfully."
        //        });
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, new ApiResponse
        //        {
        //            Code = 500,
        //            Status = "Error",
        //            Message = ex.Message
        //        });
        //    }
        //}


    }
}
