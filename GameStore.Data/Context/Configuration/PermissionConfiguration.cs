using GameStore.Data.Enum;

namespace GameStore.Data.Configuration;

public class PermissionConfiguration: IEntityTypeConfiguration<Permission>
{
    public void Configure(EntityTypeBuilder<Permission> builder)
    {
        var adminRoleId = new Guid("d27fc764-f53d-40cb-a6f9-629f433c3030");
        var managerRoleId = new Guid("aad1fa8c-b73f-40a4-8a3d-84130cf352c5");
        var moderatorRoleId = new Guid("68768561-0ed1-4cfc-a43b-7c74e635dec0");
        var userRoleId = new Guid("155f3369-d8da-4b69-8e90-72e8ac6021ce");
        var guestRoleId = new Guid("155f3369-d8da-4b69-8e90-72e8ac7022ee");

        builder.HasData(
            // Admin Permissions
            new Permission { Id = new Guid("d27fc764-f53d-40cb-a6f9-629f433c3040"), RoleId = adminRoleId, Resource = ResourceEnum.Games, AccessType = AccessTypesEnum.ReadWrite},
            new Permission { Id = new Guid("d27fc764-f53d-40cb-a6f9-629f433c3041"), RoleId = adminRoleId, Resource = ResourceEnum.Orders, AccessType = AccessTypesEnum.ReadWrite },
            new Permission { Id = new Guid("d27fc764-f53d-40cb-a6f9-629f433c3042"), RoleId = adminRoleId, Resource = ResourceEnum.Comments, AccessType = AccessTypesEnum.ReadWrite },
            new Permission { Id = new Guid("d27fc764-f53d-40cb-a6f9-629f433c3043"), RoleId = adminRoleId, Resource = ResourceEnum.Genres, AccessType = AccessTypesEnum.ReadWrite },
            new Permission { Id = new Guid("d27fc764-f53d-40cb-a6f9-629f433c3044"), RoleId = adminRoleId, Resource = ResourceEnum.Platforms, AccessType = AccessTypesEnum.ReadWrite },
            new Permission { Id = new Guid("d27fc764-f53d-40cb-a6f9-629f433c3045"), RoleId = adminRoleId, Resource = ResourceEnum.Publishers, AccessType = AccessTypesEnum.ReadWrite },
            new Permission { Id = new Guid("d27fc764-f53d-40cb-a6f9-629f433c3046"), RoleId = adminRoleId, Resource = ResourceEnum.Users, AccessType = AccessTypesEnum.ReadWrite},

            // Manager Permissions
            new Permission { Id = new Guid("d27fc764-f53d-40cb-a6f9-629f433c3050"), RoleId = managerRoleId, Resource = ResourceEnum.Games, AccessType = AccessTypesEnum.ReadWrite },
            new Permission { Id = new Guid("d27fc764-f53d-40cb-a6f9-629f433c3051"), RoleId = managerRoleId, Resource = ResourceEnum.Orders, AccessType = AccessTypesEnum.ReadWrite },
            new Permission { Id = new Guid("d27fc764-f53d-40cb-a6f9-629f433c3052"), RoleId = managerRoleId, Resource = ResourceEnum.Genres, AccessType = AccessTypesEnum.ReadWrite },
            new Permission { Id = new Guid("d27fc764-f53d-40cb-a6f9-629f433c3053"), RoleId = managerRoleId, Resource = ResourceEnum.Platforms, AccessType = AccessTypesEnum.ReadWrite },
            new Permission { Id = new Guid("d27fc764-f53d-40cb-a6f9-629f433c3054"), RoleId = managerRoleId, Resource = ResourceEnum.Publishers, AccessType = AccessTypesEnum.ReadWrite },
            new Permission { Id = new Guid("d27fc764-f53d-40cb-a6f9-629f433c3055"), RoleId = managerRoleId, Resource = ResourceEnum.Users, AccessType = AccessTypesEnum.ReadOnly }, 

            // Moderator Permissions
            new Permission { Id = new Guid("d27fc764-f53d-40cb-a6f9-629f433c3060"), RoleId = moderatorRoleId, Resource = ResourceEnum.Games, AccessType = AccessTypesEnum.ReadWrite },
            new Permission { Id = new Guid("d27fc764-f53d-40cb-a6f9-629f433c3061"), RoleId = moderatorRoleId, Resource = ResourceEnum.Orders, AccessType = AccessTypesEnum.ReadOnly }, 
            new Permission { Id = new Guid("d27fc764-f53d-40cb-a6f9-629f433c3062"), RoleId = moderatorRoleId, Resource = ResourceEnum.Orders, AccessType = AccessTypesEnum.ReadOnly }, 
            new Permission { Id = new Guid("d27fc764-f53d-40cb-a6f9-629f433c3063"), RoleId = moderatorRoleId, Resource = ResourceEnum.Genres, AccessType = AccessTypesEnum.ReadOnly }, 
            new Permission { Id = new Guid("d27fc764-f53d-40cb-a6f9-629f433c3064"), RoleId = moderatorRoleId, Resource = ResourceEnum.Platforms, AccessType = AccessTypesEnum.ReadOnly }, 
            new Permission { Id = new Guid("d27fc764-f53d-40cb-a6f9-629f433c3065"), RoleId = moderatorRoleId, Resource = ResourceEnum.Publishers, AccessType = AccessTypesEnum.ReadOnly }, 
            new Permission { Id = new Guid("d27fc764-f53d-40cb-a6f9-629f433c3066"), RoleId = moderatorRoleId, Resource = ResourceEnum.Users, AccessType = AccessTypesEnum.ReadOnly }, 

            // User Permissions
            new Permission { Id = new Guid("d27fc764-f53d-40cb-a6f9-629f433c3070"), RoleId = userRoleId, Resource = ResourceEnum.Games, AccessType = AccessTypesEnum.ReadOnly }, 
            new Permission { Id = new Guid("d27fc764-f53d-40cb-a6f9-629f433c3071"), RoleId = userRoleId, Resource = ResourceEnum.Orders, AccessType = AccessTypesEnum.ReadOnly }, 
            new Permission { Id = new Guid("d27fc764-f53d-40cb-a6f9-629f433c3072"), RoleId = userRoleId, Resource = ResourceEnum.Comments, AccessType = AccessTypesEnum.ReadOnly },
            new Permission { Id = new Guid("d27fc764-f53d-40cb-a6f9-629f433c3073"), RoleId = userRoleId, Resource = ResourceEnum.Genres, AccessType = AccessTypesEnum.ReadOnly }, 
            new Permission { Id = new Guid("d27fc764-f53d-40cb-a6f9-629f433c3074"), RoleId = userRoleId, Resource = ResourceEnum.Platforms, AccessType = AccessTypesEnum.ReadOnly }, 
            new Permission { Id = new Guid("d27fc764-f53d-40cb-a6f9-629f433c3075"), RoleId = userRoleId, Resource = ResourceEnum.Publishers, AccessType = AccessTypesEnum.ReadOnly }, 
            new Permission { Id = new Guid("d27fc764-f53d-40cb-a6f9-629f433c3076"), RoleId = userRoleId, Resource = ResourceEnum.Users, AccessType = AccessTypesEnum.ReadOnly }, 

            // Guest Permissions
            new Permission { Id = new Guid("d27fc764-f53d-40cb-a6f9-629f433c3080"), RoleId = guestRoleId, Resource = ResourceEnum.Games, AccessType = AccessTypesEnum.ReadOnly }, 
            new Permission { Id = new Guid("d27fc764-f53d-40cb-a6f9-629f433c3081"), RoleId = guestRoleId, Resource = ResourceEnum.Orders, AccessType = AccessTypesEnum.ReadOnly }, 
            new Permission { Id = new Guid("d27fc764-f53d-40cb-a6f9-629f433c3082"), RoleId = guestRoleId, Resource = ResourceEnum.Comments, AccessType = AccessTypesEnum.ReadOnly }, 
            new Permission { Id = new Guid("d27fc764-f53d-40cb-a6f9-629f433c3083"), RoleId = guestRoleId, Resource = ResourceEnum.Genres, AccessType = AccessTypesEnum.ReadOnly }, 
            new Permission { Id = new Guid("d27fc764-f53d-40cb-a6f9-629f433c3084"), RoleId = guestRoleId, Resource = ResourceEnum.Platforms, AccessType = AccessTypesEnum.ReadOnly }, 
            new Permission { Id = new Guid("d27fc764-f53d-40cb-a6f9-629f433c3085"), RoleId = guestRoleId, Resource = ResourceEnum.Publishers, AccessType = AccessTypesEnum.ReadOnly }, 
            new Permission { Id = new Guid("d27fc764-f53d-40cb-a6f9-629f433c3086"), RoleId = guestRoleId, Resource = ResourceEnum.Users, AccessType = AccessTypesEnum.ReadOnly }  
        );

    }
}