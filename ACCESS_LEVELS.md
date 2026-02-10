# Access Level Configuration

## Overview
The system uses JWT token claims combined with API-defined access types to control which menu items are visible to users. Each user has a `memberGroupWebAccessLevels` claim in their JWT token that contains an array of access level numbers they have permission to access.

## How It Works

1. **JWT Token Claim**: The `memberGroupWebAccessLevels` claim is automatically included in the JWT token when users log in
2. **API Access Types**: The full list of access types with names and descriptions is fetched from `/api/StaffAccessManagementStaffGroup/access-web-types`
3. **AccessControlService**: Reads access levels from JWT token and caches API access type definitions
4. **NavMenu**: Each menu item checks if the user has the required access level before displaying

## API Endpoint

**GET** `/api/StaffAccessManagementStaffGroup/access-web-types`
- **Authentication**: Required (Staff only)
- **Purpose**: Get all available access web types with their levels, names, and descriptions
- **Cache**: Results cached for 10 minutes

### Response Example
```json
{
  "message": "Access web types retrieved successfully",
  "accessWebTypes": [
    {
      "accessWebTypesGUID": "a777fd53-ba19-11f0-8d44-00155d0a5b1d",
      "accessWebTypesAccessLevel": 1,
      "accessWebTypesAccessLevelName": "Member Management",
      "accessWebTypesAccessLevelDescription": "Member Management"
    },
    {
      "accessWebTypesGUID": "43f34ac2-ba1a-11f0-8d44-00155d0a5b1d",
      "accessWebTypesAccessLevel": 2,
      "accessWebTypesAccessLevelName": "Refund Payment",
      "accessWebTypesAccessLevelDescription": "Refund Payment"
    },
    {
      "accessWebTypesGUID": "556e30da-ba1a-11f0-8d44-00155d0a5b1d",
      "accessWebTypesAccessLevel": 3,
      "accessWebTypesAccessLevelName": "Nyhedder",
      "accessWebTypesAccessLevelDescription": "Nyhedder"
    }
  ]
}
```

## Current Access Level Mappings

**Note**: These are temporary mappings. You can reassign the access level numbers to different menu items as needed.

| Access Level | Menu Item | Route | Current Assignment |
|--------------|-----------|-------|-------------------|
| 1 | Dashboard | `/` | All users |
| 2 | Medlemmer (Members) | `/members` | Staff only |
| 3 | Medlemskaber (Memberships) | `/staff/memberships` | Staff only |
| 4 | Personale (Staff) | `/staff-management` | Staff only |
| 5 | Hold træninger (Training Classes) | `/training-classes` | Staff/Members |
| 6 | Payments | `/payment-list` | Staff only |
| 7 | Templates | `/templates` | Staff only |
| 8 | Door Access Logs | `/access-log` | Staff only |
| 9 | Access Controller Groups | `/access-controller-groups/list` | Staff only |

### Known Access Types from API
- **1** - Member Management
- **2** - Refund Payment
- **3** - Nyhedder (News)
- **4** - Adgangs Historik (Access History)
- **5** - Hold Træning (Training Classes)
- **6** - Betalingsoversigt (Payment Overview)
- **7** - Gruppe 1
- **8** - Gruppe 2
- **9** - Gruppe 3

## Example JWT Token Response

```json
{
  "memberGuid": "f062dd17-42c6-11f0-8d44-00155d0a5b1d",
  "email": "kj@gidion.dk",
  "name": "Kim Jørgensen",
  "role": "Staff",
  "memberGroupWebAccessLevels": ["1", "2", "3", "4", "7", "8", "9"],
  "organisationMainGUID": "3a649172-42c6-11f0-8d44-00155d0a5b1d",
  "organisationSubGUID": "019744ea-aca8-72d1-b242-c9a6c287e1c6"
}
```

In this example, the user has access to:
- Dashboard (1)
- Members (2)
- Memberships (3)
- Staff Management (4)
- Templates (7)
- Door Access Logs (8)
- Access Controller Groups (9)

But does NOT have access to:
- Training Classes (5)
- Payments (6)

## Usage in Components

### Inject the Service
```csharp
@inject IAccessControlService AccessControlService
```

### Check Single Access Level
```csharp
@if (AccessControlService.HasAccessLevel(2))
{
    <MudNavLink Href="/members">Medlemmer</MudNavLink>
}
```

### Check Multiple Access Levels
```csharp
@if (AccessControlService.HasAnyAccessLevel(2, 3, 4))
{
    // Show if user has any of levels 2, 3, or 4
}
```

### Get All User Levels
```csharp
var userLevels = AccessControlService.GetUserAccessLevels();
// Returns List<int> of all access levels from JWT token
```

### Get All Available Access Types (from API)
```csharp
var allTypes = await AccessControlService.GetAllAccessWebTypesAsync();
// Returns List<AccessWebTypeDTO> with GUID, level, name, description
```

### Get Specific Access Type Info
```csharp
var typeInfo = await AccessControlService.GetAccessWebTypeByLevelAsync(2);
// Returns AccessWebTypeDTO with name "Refund Payment" for level 2
```

## Performance Notes

- **User Access Levels**: Cached from JWT token (no API calls)
- **Access Type Definitions**: Cached for 10 minutes after first API call
- Cache is cleared when user logs out (new authentication state)
- API is only called once every 10 minutes maximum

## How to Reassign Access Levels

The access level numbers can be freely reassigned to different menu items:

1. **In NavMenu.razor**: Change the number in `HasAccessLevel(X)` for any menu item
2. **Example**: To change Members from level 2 to level 5:
   ```csharp
   // Change from:
   @if (AccessControlService.HasAccessLevel(2))
   
   // To:
   @if (AccessControlService.HasAccessLevel(5))
   ```
3. The system will automatically use the user's JWT token access levels
4. No backend changes needed - just update the frontend menu checks

## Backend Integration

Access levels are managed in the backend via:
- **Staff Groups**: Created/edited via `/api/StaffAccessManagementStaffGroup/create` and `/edit`
- **Access Web Types**: Referenced by GUID when creating staff groups
- **Member Assignment**: When staff members are assigned to groups, they inherit the group's access web types
- **JWT Token**: Access levels are included in the JWT token on login

## Future Menu Items

When adding new menu items:
1. Assign a new access level number (10, 11, 12, etc.)
2. Update this documentation with the mapping
3. Use `AccessControlService.HasAccessLevel(X)` in the NavMenu
4. Ensure the backend assigns the appropriate access level to membership groups
